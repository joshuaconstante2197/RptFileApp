using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FileProcessingLibrary.Services
{
    public class SaveToDb
    {
        public bool CheckIfAccountExists(Account account)
        {
            var sql = $"SELECT 1 FROM AccountHeader WHERE ArCode  = ('{account.AccountHeader.ArCode}')";
            using (SqlConnection sqlCon = new SqlConnection(Config.ConnString))
            {

            }
            return true;

        }
        public bool SaveAccountHeader(Account account)
        {
            var sql = $"INSERT INTO AccountHeader(ArCode) SELECT('{account.AccountHeader.ArCode}') WHERE ";
            using (SqlConnection sqlcon = new SqlConnection(Config.ConnString))
            {

            }
            return true;

        }
    }
}
