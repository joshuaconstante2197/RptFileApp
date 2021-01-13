using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace FileProcessingLibrary
{
    public static class Config
    {
        public static string ConnString = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;
        public static string DataPath = @"..\FileProcessingLibrary\Data\";
        public static string pathToRptFile = @"..\..\..\..\FileProcessingLibrary\Data\011321.txt";
        public static string pathToRptNewFile = @"..\..\..\..\FileProcessingLibrary\Data\121520.txt";


        public static string WebDataPath = @"..\FileProcessingLibrary\Data\";
    }
}