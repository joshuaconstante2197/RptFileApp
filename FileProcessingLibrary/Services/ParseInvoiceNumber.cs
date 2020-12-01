using System;
using System.Collections.Generic;
using System.Text;

namespace FileProcessingLibrary
{
    public class ParseInvoiceNumber
    {
        public static void Parse(string str, List<string> invList)
        {
            if(str.Length <= 8)
            {
                Balance(str, invList);
            }
            else if(str.Length > 8 && str.Length <= 21)
            {
                Current(str, invList);
            }
            else if(str.Length >= 22 && str.Length <= 32)
            {
                Over30(str, invList);
            }
            else if(str.Length >= 33 && str.Length <= 45)
            {
                Over60(str, invList);
            }
            else if(str.Length >= 47)
            {
                Over90(str, invList);
            }
            else
            {
                throw new ArgumentOutOfRangeException("something is wrong with invoice line" + str.Length);
            }
        }
        private static void Balance(string str, List<string> invList)
        {
            invList.Add(str.Trim().TrimEnd());
        }

        private static void Current(string str, List<string> invList)
        {
                invList.Add(str.Substring(0, 9).Trim().TrimEnd());
                invList.Add(str.Substring(10, str.Length - 10).Trim().TrimEnd());
        }
        private static void Over30(string str, List<string> invList)
        {
            Current(str.Substring(0, 21),invList);
            invList.Add(str.Substring(22, str.Length - 22).Trim().TrimEnd());
        }
        private static void Over60(string str, List<string> invList)
        {
            Over30(str.Substring(0, 33), invList);
            invList.Add(str.Substring(33, str.Length - 33).Trim().TrimEnd());
        }
        private static void Over90(string str, List<string> invList)
        {
            Over60(str.Substring(0, 45), invList);
            invList.Add(str.Substring(46, str.Length - 46).Trim().TrimEnd());
        }

    }
}
