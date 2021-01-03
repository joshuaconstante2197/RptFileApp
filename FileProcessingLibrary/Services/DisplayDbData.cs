using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FileProcessingLibrary.Services
{
    public class DisplayDbData
    {
        public List<AccountHeader> DisplayAllAccounts()
        {
            var accounts = new List<AccountHeader>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    var sql = "SELECT * FROM AccountHeader";
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
                err.ErrorLog(Config.WebDataPath + "err.log", ex.Message);
                throw;
            }
            
            return accounts;
        }
        public List<AccountInfo> DisplayAccountInfo(string arCode)
        {
            var accounts = new List<AccountInfo>();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    var sql = $"SELECT * FROM AccountInfo WHERE ArCode = '{arCode}'";
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
                Err.ErrorLog(Config.WebDataPath + "err.log","Error on Displaying account info" + ex.Message);
                throw;
            }

            return accounts;
        }
        public AccountHeader GetAccountHeaderByArCode(string arCode)
        {
            var accountHeader = new AccountHeader();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    var sql = $"SELECT * FROM AccountHeader WHERE ArCode = '{arCode}'";
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
                Err.ErrorLog(Config.WebDataPath + "err.log", "Error on GetAccountHeaderByArCode" + ex.Message );
                throw;
            }
            return accountHeader;
        }
        public InvoiceBalance GetInvoiceBalance(string arCode, string transactionId)
        {
            var invoiceBalance = new InvoiceBalance();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    var sql = $"SELECT DISTINCT * FROM InvoiceBalance WHERE ArCode = '{arCode}' AND TransactionId = {transactionId}";
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
                Err.ErrorLog(Config.WebDataPath + "err.log", "Error on GetInvoiceBalance" + ex.Message);
                throw;
            }
            return invoiceBalance;
        }
    }
}
