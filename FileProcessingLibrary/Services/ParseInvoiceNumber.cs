using System;
using System.Collections.Generic;
using System.Text;

namespace FileProcessingLibrary
{
    public class ParseInvoiceNumber
    {
        public static void Parse(string str, List<string> invList)
        {
            if(str.Length <= 9)
            {
                Balance(str, invList);
            }
            else if(str.Length > 9 && str.Length <= 22)
            {
                Current(str, invList);
            }
            else if(str.Length >= 23 && str.Length <= 33)
            {
                Over30(str, invList);
            }
            else if(str.Length >= 34 && str.Length <= 46)
            {
                Over60(str, invList);
            }
            else if(str.Length >= 48)
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
                invList.Add(str.Substring(0, 10).Trim().TrimEnd());
                invList.Add(str.Substring(11, str.Length - 11).Trim().TrimEnd());
        }
        private static void Over30(string str, List<string> invList)
        {
            Current(str.Substring(0, 22),invList);
            invList.Add(str.Substring(23, str.Length - 23).Trim().TrimEnd());
        }
        private static void Over60(string str, List<string> invList)
        {
            Over30(str.Substring(0, 34), invList);
            invList.Add(str.Substring(34, str.Length - 34).Trim().TrimEnd());
        }
        private static void Over90(string str, List<string> invList)
        {
            Over60(str.Substring(0, 46), invList);
            invList.Add(str.Substring(47, str.Length - 47).Trim().TrimEnd());
        }

    }
}
