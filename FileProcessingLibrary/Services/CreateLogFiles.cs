using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileProcessingLibrary.Services
{
    public class CreateLogFiles
    {
        private string sLogFormat;
        private string sErrorTime;
        public CreateLogFiles()
        {

            sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";


            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            sErrorTime = sYear + "-" + sMonth + "-" + sDay;
        }
        public void ErrorLog(string sPathName, string err)
        {
            StreamWriter sw = new StreamWriter(sPathName + sErrorTime, true);
            sw.WriteLine(sLogFormat + err);
            sw.Flush();
            sw.Close();
        }
    }
}
