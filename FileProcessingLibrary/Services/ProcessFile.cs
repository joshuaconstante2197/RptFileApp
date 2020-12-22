using FileProcessingLibrary.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FileProcessingLibrary
{
    public static class ProcessFile
    {
        public static void DeleteEmptiesAndNonArs(string pathToRptFile, string pathToTempFile)
        {
            File.WriteAllText(pathToTempFile, string.Empty);
            string line;
            bool count = false;
            bool wordToEx = true;
            string[] wordsToExclude = { "T R I A L", "Tran    Tran", "Date    Descr", "----    -----", "Time", "---------","***" };
            StreamReader rptFile = new StreamReader(pathToRptFile);

            int k;
            //making sure I haven't reached the end of the line
            while ((rptFile.Peek()) >= 0)
            {
                //read the charater
                k = rptFile.Read();
                //if reachead end of the line check if next character is 
                if (k == 10)
                {
                    //every AR starts with an A 
                    if (rptFile.Peek() == 65)
                    {
                        break;
                    }
                }

            }

            using (StreamWriter streamWriter = new StreamWriter(pathToTempFile))
            {
                while ((line = rptFile.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        foreach (var word in wordsToExclude)
                        {
                            //making sure the line I'm processing does not contain the characters to exclude
                            if (line.Contains(word))
                            {
                                wordToEx = false;
                                break;
                            }
                            //also making sure the line does not have single quotes since that will mess up inserting into db
                            else if(line.Contains("\'"))
                            {
                               line = line.Replace("\'", " ");
                            }
                            wordToEx = true;
                        }
                        if (wordToEx)
                        {
                            if (!char.IsDigit(line[0]) && !char.IsWhiteSpace(line[0]) && line[0] != 'E')
                            {
                                streamWriter.WriteLine(line);
                                //count is set to true so if a line starts with an E (not an AR account) the streamwriter will ignore it
                                count = true;
                            }
                            else if (char.IsWhiteSpace(line[0]) && count)
                            {
                                streamWriter.WriteLine(line);
                            }
                            else if (char.IsDigit(line[0]) || line[0] == 'E')
                            {
                                count = false;
                            }

                        }
                        else
                        {
                            continue;
                        }
                    }

                }
            }
        }

        public static AccountHeader SetAccountHeader(string header)
        {
            var accountHeader = new AccountHeader();
            var accountN = string.Empty;

            for (int i = 0; i < header.Length; i++)
            {
                //ARcode will allways be the first word on the account header
                if(i <  header.IndexOf(' '))
                {
                    accountHeader.ArCode += string.Concat(header[i]);
                    continue;
                }
                else
                {
                    //a header line may have an opening parenthesis with a word or may not contain an opening parenthesis at all which would mean that line doesn't have a phone number
                    if (i < header.LastIndexOf('(') || !header.Contains('('))
                    {
                        accountN += string.Concat(header[i]);
                        continue;
                    }
                    accountHeader.AccountPhoneNumber += string.Concat(header[i]);
                }
            }
            //clean the account name before setting the object since it may have extra spaces
            string[] strArr = accountN.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArr.Length; i++)
            {
                accountHeader.AccountName += string.Concat(strArr[i], ' ');
            }
            return accountHeader;
        }

        public static AccountInfo SetAccountInfo(string line)
        {
            var accountInfo = new AccountInfo();

            if (!line.Contains("TOTAL"))
            {
                if (line.Substring(18, 9).Contains('/'))
                {
                    accountInfo.TranDate = Convert.ToDateTime(line.Substring(18, 9));
                }
                
                if (!string.IsNullOrWhiteSpace(line.Substring(28, 13)))
                {
                    if (char.IsDigit(line[42]))
                    {
                        accountInfo.TranDetail = line.Substring(28, 11);
                    }
                    else
                    {
                        accountInfo.TranDetail = line.Substring(28, 13);
                    }
                }

                if (!line.Substring(39, 10).Contains('T') && !char.IsWhiteSpace(line[44]))
                {
                    accountInfo.InvoiceNumber = line.Substring(39, 10);
                }

                if (!string.IsNullOrWhiteSpace(line.Substring(49, 13)) && !line.Substring(49, 13).Contains("Customer"))
                {
                    accountInfo.ReferenceNumber = line.Substring(49, 13).TrimStart();
                }
                if (!string.IsNullOrWhiteSpace(line.Substring(64, 7)) && line.Substring(64,7).Contains('/'))
                {
                    accountInfo.DueDate = Convert.ToDateTime(line.Substring(63, 8).TrimStart());
                }
            }
            else
            {
                accountInfo.TranDetail = "Total Customer";
            }
            return accountInfo;
        }
        public static InvoiceBalance SetInvoiceBalance(string line)
        {
            var invoiceBalances = new InvoiceBalance();
            var vs = new List<string>();

            ParseInvoiceNumber.Parse(line.Substring(74, line.Length - 74),vs);
            PropertyInfo[] properties = invoiceBalances.GetType().GetProperties();

            for (int i = 0; i < vs.Count; i++)
            {
                if (!string.IsNullOrEmpty(vs[i]))
                {
                    properties[i].SetValue(invoiceBalances, vs[i].Last() == '-' ? (Convert.ToDecimal(vs[i].Remove(vs[i].Length - 1)) * -1) : Convert.ToDecimal(vs[i]));
                }
                else
                {
                    properties[i].SetValue(invoiceBalances, 0.0m);
                }
            }
            return invoiceBalances;
        }

        public static Account SetAccount(string line, Account account)
        {
            if (!char.IsWhiteSpace(line[0]))
            {
                account.AccountHeader = SetAccountHeader(line);
            }
            else 
            {
                account.AccountInfo.Add(SetAccountInfo(line));
                if (line.Substring(74, line.Length - 74).Any(char.IsDigit))
                {
                    account.Balances.Add(SetInvoiceBalance(line));
                }
            }

            return account;
        }

        public static void Process(string pathToRptFile, string pathToTempFile)
        {
            string line;
            var listOfAccounts = new List<Account>();

            Account account = new Account();
            var newAccount = new SaveToDb();

            DeleteEmptiesAndNonArs(pathToRptFile, pathToTempFile);

            StreamReader cleanFile = new StreamReader(pathToTempFile);
            while ((line = cleanFile.ReadLine()) != null)
            {
                if(!char.IsWhiteSpace(line[0]))
                {
                    if(account.AccountHeader != null)
                    {
                        newAccount = new SaveToDb();
                        newAccount.SaveAccountHeader(account);
                        if (account.AccountInfo.Count > 0)
                        {
                            newAccount.SaveAccountInfo(account);
                        }
                    }
                    account = new Account();
                }
                SetAccount(line, account);
                //run method to save on database
            }
            //foreach (var ac in listOfAccounts)
            //{
            //    if(ac.AccountHeader != null)
            //    {
            //        Console.WriteLine(ac.AccountHeader.ArCode);
            //        foreach (var acInfo in ac.AccountInfo)
            //        {
            //            Console.WriteLine(acInfo.InvoiceNumber);
            //        }
            //    }
                
            //}
        }
    }
}
