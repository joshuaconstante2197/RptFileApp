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
                Err.ErrorLog(Config.WebDataPath + "err.log", ex.Message);
                throw;
            }

            return accounts;
        }

    }
}
