using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace FileProcessingLibrary.Services
{
    public class SaveToDb
    {
        public bool CheckIfAccountHeaderExists(Account account)
        {
            var sql = $"SELECT 1 FROM AccountHeader WHERE ArCode  = ('{account.AccountHeader.ArCode}')";
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                using (SqlCommand cmd = new SqlCommand(sql,sqlCon))
                {
                    var reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

        }
        public void DeletePaidAccounts(Account account)
        {
            foreach (var accountInfo in account.AccountInfo)
            {
                var sql = "IF EXISTS " +
                            $"(SELECT * FROM AccountInfo WHERE ArCode  = ('{account.AccountHeader.ArCode}') AND TranDate = ('{accountInfo.TranDate}') AND TranDetail = ('{accountInfo.TranDetail}') AND InvoiceNumber = ('{accountInfo.InvoiceNumber}')) " +
                          "BEGIN " +
                            $"DELETE FROM AccountInfo WHERE ArCode  = ('{account.AccountHeader.ArCode}') AND TranDate = ('{accountInfo.TranDate}') AND TranDetail = ('{accountInfo.TranDetail}') AND InvoiceNumber = ('{accountInfo.InvoiceNumber}') " +
                          "END";
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                    {
                        try
                        {
                            var i = cmd.ExecuteNonQuery();
                             i += 0;
                            if (i > 1)
                            {
                                i += 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            var err = new CreateLogFiles();
                            err.ErrorLog(Config.DataPath, "Error deleting this record:" + sql + "Error message:" + ex);
                            throw;
                        }
                    }
                }
            }
        }

        public bool SaveAccountHeader(Account account)
        {
            string sql;
            if (account != null && !CheckIfAccountHeaderExists(account))
            {
                sql = $"INSERT INTO AccountHeader(ArCode, AccountName, AccountPhoneNumber) VALUES ('{account.AccountHeader.ArCode}','{account.AccountHeader.AccountName}','{account.AccountHeader.AccountPhoneNumber}') ";
            }
            else
            {
                return false;
            }
             
            using (SqlConnection sqlcon = new SqlConnection(Config.ConnString))
            {
                sqlcon.Open();
                using (SqlCommand cmd = new SqlCommand(sql, sqlcon))
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        var err = new CreateLogFiles();
                        err.ErrorLog(Config.DataPath + "err.log ", "Error inserting account header " + "Error on this command" + sql + ex );
                        return false;
                        throw;
                    }
                }
            }
        }
        public bool SaveAccountInfo(Account account)
        {
            foreach (var accountInfo in account.AccountInfo)
            {
                var sql = $"INSERT INTO AccountInfo(ArCode, TranDate, TranDetail, DueDate, InvoiceNumber, ReferenceNumber)  VALUES('{account.AccountHeader.ArCode}','{accountInfo.TranDate}','{accountInfo.TranDetail}'," +
                          $"'{accountInfo.DueDate}','{accountInfo.InvoiceNumber}','{accountInfo.ReferenceNumber}')";
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand(sql,sqlCon))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            var err = new CreateLogFiles();
                            err.ErrorLog(Config.DataPath + "err.log", ex + "Error with this line: " + sql);
                            return false;
                            throw;
                        }
                    }
                }
            }
            return true;
        }
        public bool SaveAccountBalances(Account account)
        {
            
            var transactionId = GetTransactionIds(account.AccountHeader.ArCode);
            int i = 0;
            foreach (var accountBalance in account.Balances)
            {
                var sql = $"INSERT INTO InvoiceBalance(ArCode,TransactionId, Balance,Curr,Over30,Over60,Over90) VALUES('{accountBalance.ArCode}',{transactionId[i]},{accountBalance.Balance},{accountBalance.Current}," +
                          $"{accountBalance.Over30},{accountBalance.Over60},{accountBalance.Over90})";
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            var err = new CreateLogFiles();
                            err.ErrorLog(Config.DataPath + "err.log", ex + "Error saving account balances int this line: " + sql);
                            return false;
                            throw;
                        }
                    }
                }
                i++;

            }
            return true;
        }
        private List<int> GetTransactionIds(string ArCode)
        {
            int id;
            List<int> ids = new List<int>();
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                var sql = $"SELECT TransactionId FROM AccountInfo WHERE ArCode = '{ArCode}' ";
                using (SqlCommand cmd = new SqlCommand(sql,sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = 0;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                id = Convert.ToInt32(reader.GetValue(i));
                            }
                            ids.Add(id);
                        }
                    }
                }
            }
            return ids;
        }
        private static Byte[] ConvertFileToBytes(string pathToTempFile)
        {

            Byte[] bytes;
            using (FileStream fs = new FileStream(pathToTempFile, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    try
                    {
                        bytes = br.ReadBytes((Int32)fs.Length);

                    }
                    catch (Exception ex)
                    {
                        var err = new CreateLogFiles();
                        err.ErrorLog(Config.DataPath + "err.log", ex + "Error file to DB");
                        throw;
                    }
                }
            }
            return bytes;
        }

        public bool SaveFileToDb(string pathToTempFile, ProcessFile.TypeOfFile typeOfFile)
        {
            Byte[] bytes = ConvertFileToBytes(pathToTempFile);
            string fileName = Path.GetFileName(pathToTempFile);
            string fileExtension = Path.GetExtension(pathToTempFile);
            DateTime dateTime = DateTime.Now;

            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                string sql = $"INSERT INTO Files(FileName,FileExtension,DataFile,CreatedOn,TypeOfFile) VALUES(@FileName, @FileExtension,@DataFile,@CreatedOn,@TypeOfFile)";
                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    try
                    {
                        cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = fileName;
                        cmd.Parameters.Add("@FileExtension", SqlDbType.VarChar).Value = fileExtension;
                        cmd.Parameters.Add("@DataFile", SqlDbType.Binary).Value = bytes;
                        cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = dateTime;
                        cmd.Parameters.Add("@TypeOfFile", SqlDbType.VarChar).Value = typeOfFile;

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        var err = new CreateLogFiles();
                        err.ErrorLog(Config.DataPath + "err.log", ex + "Error saving file to DB: " + sql);
                        return false;
                        throw;
                    }
                }
            }
            return true;
        }
    }
}
