using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FileProcessingLibrary.Services
{
    public class CurateDb
    {
        private static List<string> GetNegativeAndZeroArs()
        {
            var ars = new List<string>();
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                var sql = "SELECT AccountInfo.ArCode FROM AccountInfo LEFT JOIN InvoiceBalance ON  AccountInfo.TransactionId = InvoiceBalance.TransactionId " +
                            "WHERE AccountInfo.TranDetail = 'Total Customer' AND InvoiceBalance.Balance <= 0.01 " +
                            "UNION " +
                            "SELECT ArCode FROM AccountHeader WHERE ArCode NOT IN(SELECT ArCode FROM AccountInfo)";
;
                using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                {
                    sqlCon.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            string ar = string.Empty;

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                ar = reader.GetValue(i).ToString();
                            }
                            ars.Add(ar);
                        }
                    }
                }
            }
            return ars;
        }
        public static bool DeleteNegativeAndZeroAccounts1()
        {
            var ars = GetNegativeAndZeroArs();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
                {
                    sqlCon.Open();
                    foreach (var ar in ars)
                    {
                        var sql = $"DELETE FROM Accountheader WHERE Arcode = '{ar}'";
                        using (SqlCommand cmd = new SqlCommand(sql, sqlCon))
                        {
                            cmd.ExecuteNonQuery();
                        }

                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                var errLog = new CreateLogFiles();
                errLog.ErrorLog(Config.DataPath, "Error curating DB" + ex.Message);
                return false;
                throw;
            }

        }
        public static bool DeleteNegativeAndZeroAccounts()
        {
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {
                sqlCon.Open();
                var spGetNegativesAndZeroAccounts = "dbo.spDeleteNegativeAndZeroAccounts";
                using (SqlCommand cmd = new SqlCommand(spGetNegativesAndZeroAccounts,sqlCon))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        var errLog = new CreateLogFiles();
                        errLog.ErrorLog(Config.DataPath, "Error curating DB" + ex.Message);
                        return false;
                        throw;
                    }
                }
            }
        }
    }
}
