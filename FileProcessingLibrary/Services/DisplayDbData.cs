using FileProcessingLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace FileProcessingLibrary.Services
{
    public class DisplayDbData
    {
        public List<AccountHeader> DisplayAllAccounts()
        {
            var accounts = new List<AccountHeader>();
            string sql = string.Empty;

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    sql = "SELECT * FROM AccountHeader";
                    using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var accountHeader = new AccountHeader();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var fieldName = reader.GetName(i);
                                    var property = accountHeader.GetType().GetProperty(fieldName);

                                    if (property != null && !reader.IsDBNull(i))
                                    {
                                        property.SetValue(accountHeader, reader.GetValue(i), null);
                                    }
                                }
                                accounts.Add(accountHeader);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var err = new CreateLogFiles();
                err.ErrorLog(Config.WebDataPath + "err.log", ex.Message + "Error on this query" + sql);
                throw;
            }

            return accounts;
        }
        public List<AccountInfo> DisplayAccountInfo(string arCode)
        {
            var accounts = new List<AccountInfo>();
            string sql = string.Empty;

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    sql = $"SELECT * FROM AccountInfo WHERE ArCode = '{arCode}'";
                    using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var accountInfo = new AccountInfo();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var fieldName = reader.GetName(i);
                                    var property = accountInfo.GetType().GetProperty(fieldName);

                                    if (property != null && !reader.IsDBNull(i))
                                    {
                                        property.SetValue(accountInfo, reader.GetValue(i), null);
                                    }
                                }
                                accounts.Add(accountInfo);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var Err = new CreateLogFiles();
                Err.ErrorLog(Config.WebDataPath + "err.log", "Error on Displaying account info" + ex.Message + "Error on this query" + sql);
                throw;
            }

            return accounts;
        }
        public AccountHeader GetAccountHeaderByArCode(string arCode)
        {
            var accountHeader = new AccountHeader();
            string sql = string.Empty;

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    sql = $"SELECT * FROM AccountHeader WHERE ArCode = '{arCode}'";
                    using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var fieldName = reader.GetName(i);
                                    var property = accountHeader.GetType().GetProperty(fieldName);

                                    if (property != null && !reader.IsDBNull(i))
                                    {
                                        property.SetValue(accountHeader, reader.GetValue(i), null);
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var Err = new CreateLogFiles();
                Err.ErrorLog(Config.WebDataPath + "err.log", "Error on GetAccountHeaderByArCode" + ex.Message + "Error on this query" + sql);
                throw;
            }
            return accountHeader;
        }
        public InvoiceBalance GetInvoiceBalance(string arCode, string transactionId)
        {
            var invoiceBalance = new InvoiceBalance();
            string sql = string.Empty;

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    sql = $"SELECT DISTINCT * FROM InvoiceBalance WHERE ArCode = '{arCode}' AND TransactionId = {transactionId}";
                    using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var fieldName = reader.GetName(i);
                                    var property = invoiceBalance.GetType().GetProperty(fieldName);

                                    if (property != null && !reader.IsDBNull(i))
                                    {
                                        property.SetValue(invoiceBalance, reader.GetValue(i), null);
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var Err = new CreateLogFiles();
                Err.ErrorLog(Config.WebDataPath + "err.log", "Error on GetInvoiceBalance" + ex.Message + "Error on this query" + sql);
                throw;
            }
            return invoiceBalance;
        }
        public decimal GetTotalBalance(string ArCode)
        {
            var balance = new decimal();
            string sql = string.Empty; 
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    sql = $"SELECT DISTINCT Balance FROM InvoiceBalance WHERE ArCode = '{ArCode}' AND TransactionId = (SELECT TransactionId From AccountInfo WHERE ArCode = '{ArCode}' AND TranDetail = 'Total Customer')";
                    using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.FieldCount == 1 && !reader.IsDBNull(0))
                                {
                                    balance = Convert.ToDecimal(reader.GetValue(0));
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var Err = new CreateLogFiles();
                Err.ErrorLog(Config.WebDataPath + "err.log", "Error on GetTotalBalance" + ex.Message + "Error on this query" + sql);
                throw;
            }
            return balance;
        }
        public decimal GetTranBalance(string ArCode, int TranId)
        {
            var balance = new decimal();
            string sql = string.Empty;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    sql = $"SELECT DISTINCT Balance FROM InvoiceBalance WHERE ArCode = '{ArCode}' AND TransactionId = {TranId}";
                    using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader.FieldCount == 1 && !reader.IsDBNull(0))
                                {
                                    balance = Convert.ToDecimal(reader.GetValue(0));
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var Err = new CreateLogFiles();
                Err.ErrorLog(Config.WebDataPath + "err.log", "Error on GetTranBalance" + ex.Message + "Error on this query" + sql);
                throw;
            }
            return balance;
        }
        public string DownloadFileToProjectFolder(string pathToData)
        {
            //the index number to write bytes to  
            long CurrentIndex = 0;

            //the number of bytes to store in the array  
            int BufferSize = 100;

            //The Number of bytes returned from GetBytes() method  
            long BytesReturned;

            //A byte array to hold the buffer  
            byte[] Blob = new byte[BufferSize];
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    var sql = "IF EXISTS (SELECT TOP 1 * FROM Files WHERE TypeOfFile = 'newData' ORDER BY DocumentId DESC) " + 
                                "BEGIN " +
                                    "SELECT TOP 1 * FROM Files WHERE TypeOfFile = 'newData' ORDER BY DocumentId DESC " + 
                                "END";
                    using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            while (reader.Read())
                            {
                                FileStream fs = new FileStream(pathToData + "\\" + reader["FileName"].ToString(), FileMode.OpenOrCreate, FileAccess.Write);
                                BinaryWriter writer = new BinaryWriter(fs);

                                //reset the index to the beginning of the file  
                                CurrentIndex = 0;
                                //the BlobsTable column indexCurrentIndex, 
                                // the current index of the field from which to begin the read operationBlob, 
                                // Array name to write tha buffer to0, 
                                // the start index of the array to start the write operationBufferSize 
                                // the maximum length to copy into the buffer);
                                BytesReturned = reader.GetBytes(3, CurrentIndex, Blob, 0, BufferSize);

                                while (BytesReturned == BufferSize)
                                {
                                    writer.Write(Blob);
                                    writer.Flush();
                                    CurrentIndex += BufferSize;
                                    BytesReturned = reader.GetBytes(3, CurrentIndex, Blob, 0, BufferSize);
                                }
                                var fileName = fs.Name;
                                writer.Write(Blob, 0, (int)BytesReturned); writer.Flush(); writer.Close(); fs.Close();
                                return fileName;
                            }
                        }
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                var Err = new CreateLogFiles();
                Err.ErrorLog(Config.WebDataPath + "err.log", "Error on Downloding previos file" + ex.Message);
                return string.Empty;
                throw;
            }
            
        }
        public FileStream DownloadFileToProjectFolder(string pathToData, int id)
        {
            //the index number to write bytes to  
            long CurrentIndex = 0;

            //the number of bytes to store in the array  
            int BufferSize = 100;

            //The Number of bytes returned from GetBytes() method  
            long BytesReturned;

            //A byte array to hold the buffer  
            byte[] Blob = new byte[BufferSize];
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    var sql = $"SELECT * FROM Files WHERE DocumentId = {id}";
                    using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            while (reader.Read())
                            {
                                FileStream fs = new FileStream(pathToData + "\\" + reader["FileName"].ToString(), FileMode.OpenOrCreate, FileAccess.Write);
                                BinaryWriter writer = new BinaryWriter(fs);

                                //reset the index to the beginning of the file  
                                CurrentIndex = 0;
                                //the BlobsTable column indexCurrentIndex, 
                                // the current index of the field from which to begin the read operationBlob, 
                                // Array name to write tha buffer to0, 
                                // the start index of the array to start the write operationBufferSize 
                                // the maximum length to copy into the buffer);
                                BytesReturned = reader.GetBytes(3, CurrentIndex, Blob, 0, BufferSize);

                                while (BytesReturned == BufferSize)
                                {
                                    writer.Write(Blob);
                                    writer.Flush();
                                    CurrentIndex += BufferSize;
                                    BytesReturned = reader.GetBytes(3, CurrentIndex, Blob, 0, BufferSize);
                                }
                                var fileName = fs.Name;
                                writer.Write(Blob, 0, (int)BytesReturned); writer.Flush(); writer.Close(); fs.Close();
                                var file = File.OpenRead(fileName);
                                return file;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var Err = new CreateLogFiles();
                Err.ErrorLog(Config.WebDataPath + "err.log", "Error on Downloding previos file" + ex.Message);
                return null;
                throw;
            }
            return null;

        }
        public List<Files> GetAllFiles()
        {
            var files = new List<Files>();
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                var sql = "SELECT * FROM Files";
                using (SqlCommand cmd = new SqlCommand(sql,sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var file = new Files();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var fieldName = reader.GetName(i);
                                var property = file.GetType().GetProperty(fieldName);

                                if (property != null && !reader.IsDBNull(i))
                                {
                                    property.SetValue(file, reader.GetValue(i), null);
                                }
                            }
                            files.Add(file);
                        }

                    }
                }
            }
            return files;
        }
        
    }
}
