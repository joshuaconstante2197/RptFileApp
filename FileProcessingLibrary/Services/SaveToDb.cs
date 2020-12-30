using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public bool SaveAccountHeader(Account account)
        {
            var sql = String.Empty;
            
            
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
                var sql = $"INSERT INTO AccountInfo(TransactionId, ArCode, TranDate, TranDetail, DueDate, InvoiceNumber, ReferenceNumber)  VALUES({accountInfo.TransactionId},'{account.AccountHeader.ArCode}','{accountInfo.TranDate}','{accountInfo.TranDetail}'," +
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
            foreach (var accountBalance in account.Balances)
            {
                var sql = $"INSERT INTO InvoiceBalance(ArCode,TransactionId,Balance,Curr,Over30,Over60,Over90) VALUES('{accountBalance.ArCode}',{accountBalance.TransactionId},{accountBalance.Balance},{accountBalance.Current}," +
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
            }
            return true;
        }
    }
}
