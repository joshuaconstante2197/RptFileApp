﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class Backup
    {
        //string line;
        //ProcessFile.DeleteEmptiesAndNonArs(pathToRptFile, pathToTempFile);

        //StreamReader cleanFile = new StreamReader(pathToTempFile);
        //while ((line = cleanFile.ReadLine()) != null)
        //{
        //    if (!char.IsWhiteSpace(line[0]))
        //    {
        //        var accountHeader = new AccountHeader();
        //        accountHeader = ProcessFile.SetAccountHeader(line);
        //    }
        //    else if (!line.Contains("TOTAL"))
        //    {
        //        var accountInfo = new AccountInfo();
        //        if (line.Substring(18, 9).Contains('/'))
        //        {
        //            accountInfo.TranDate = Convert.ToDateTime(line.Substring(18, 9));
        //        }
        //        string tranDescription = string.Empty;
        //        if (!string.IsNullOrWhiteSpace(line.Substring(28, 13)))
        //        {
        //            if (char.IsDigit(line[42]))
        //            {
        //                accountInfo.TranDetail = line.Substring(28, 11);
        //            }
        //            else
        //            {
        //                accountInfo.TranDetail = line.Substring(28, 13);
        //            }
        //        }

        //        var invoice = string.Empty;
        //        if (!line.Substring(39, 10).Contains('T') && !char.IsWhiteSpace(line[44]))
        //        {
        //            accountInfo.InvoiceNumber = line.Substring(39, 10);
        //        }


        //        var referenceNumber = string.Empty;
        //        if (!string.IsNullOrWhiteSpace(line.Substring(49, 13)) && !line.Substring(49, 13).Contains("Customer"))
        //        {
        //            accountInfo.ReferenceNumber = line.Substring(49, 13).TrimStart();
        //        }
        //        var dueDate = string.Empty;
        //        if (!string.IsNullOrWhiteSpace(line.Substring(64, 7)))
        //        {
        //            accountInfo.DueDate = Convert.ToDateTime(line.Substring(63, 8).TrimStart());
        //        }

        //        var inv = new InvoiceBalance();

        //        if (line.Substring(74, line.Length - 74).Any(char.IsDigit))
        //        {
        //            List<string> vs = ParseInvoiceNumber.Parse(line.Substring(74, line.Length - 74));
        //            PropertyInfo[] properties = inv.GetType().GetProperties();

        //            for (int i = 0; i < vs.Count; i++)
        //            {
        //                if (!string.IsNullOrEmpty(vs[i]))
        //                {
        //                    properties[i].SetValue(inv, vs[i].Last() == '-' ? (Convert.ToDecimal(vs[i].Remove(vs[i].Length - 1)) * -1) : Convert.ToDecimal(vs[i]));
        //                }
        //                else
        //                {
        //                    properties[i].SetValue(inv, 0.0m);
        //                }
        //            }

        //        }

        //    }
        //    else
        //    {
        //        Console.WriteLine("Total Customer: ");


        //        var inv = new InvoiceBalance();
        //        if (line.Substring(74, line.Length - 74).Any(char.IsDigit))
        //        {
        //            List<string> vs = ParseInvoiceNumber.Parse(line.Substring(74, line.Length - 74));
        //            PropertyInfo[] properties = inv.GetType().GetProperties();

        //            for (int i = 0; i < vs.Count; i++)
        //            {
        //                if (!string.IsNullOrEmpty(vs[i]))
        //                {
        //                    properties[i].SetValue(inv, vs[i].Last() == '-' ? (Convert.ToDecimal(vs[i].Remove(vs[i].Length - 1)) * -1) : Convert.ToDecimal(vs[i]));
        //                }
        //                else
        //                {
        //                    properties[i].SetValue(inv, 0.0m);
        //                }
        //            }

        //        }

        //    }
        //}
    }
}
