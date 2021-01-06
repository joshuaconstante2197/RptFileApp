using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        private DataTable PutPreviosFileOnDataTable(string pathToData)
        {
            //the index number to write bytes to  
            long CurrentIndex = 0;

            //the number of bytes to store in the array  
            int BufferSize = 100;

            //The Number of bytes returned from GetBytes() method  
            long BytesReturned;

            //A byte array to hold the buffer  
            byte[] Blob = new byte[BufferSize];
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                var sql = "SELECT TOP 1 * FROM Files ORDER BY ID DESC";
                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FileStream fs = new FileStream(pathToData + "\\" + reader["BlobFileName"].ToString(), FileMode.OpenOrCreate, FileAccess.Write );
                            BinaryWriter writer = new BinaryWriter(fs);

                            //reset the index to the beginning of the file  
                            CurrentIndex = 0;
                            //the BlobsTable column indexCurrentIndex, 
                            // the current index of the field from which to begin the read operationBlob, 
                            // Array name to write tha buffer to0, 
                            // the start index of the array to start the write operationBufferSize 
                            // the maximum length to copy into the buffer);   
                            while (BytesReturned == BufferSize)
                            {
                                writer.Write(Blob);
                                writer.Flush();
                                CurrentIndex += BufferSize;
                                BytesReturned = reader.GetBytes(1, CurrentIndex, Blob, 0, BufferSize);
                            }
                            writer.Write(Blob, 0, (int)BytesReturned); writer.Flush(); writer.Close(); fs.Close();
                        }
                    }
                    }
                }
            }
        }
        
    }
}
