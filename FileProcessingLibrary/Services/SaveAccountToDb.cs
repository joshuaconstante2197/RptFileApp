using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace FileProcessingLibrary.Services
{
    public class SaveAccountToDb
    {
        public static void SaveAccountHeader(Account account)
        {
            SqlCommand cmd;
            var sql = string.Empty;
            PropertyInfo[] accountHeaderProperties = (new AccountHeader()).GetType().GetProperties();
            foreach (var property in accountHeaderProperties)
            {
                  sql += $"INSERT INTO AccountHeader" + property.Name;
            }
        }
    }
}
