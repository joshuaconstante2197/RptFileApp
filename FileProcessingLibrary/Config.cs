using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace FileProcessingLibrary
{
    public static class Config
    {
        public static string ConnString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;
    }
}
