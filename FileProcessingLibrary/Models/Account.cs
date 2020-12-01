using System;
using System.Collections.Generic;
using System.Text;

namespace FileProcessingLibrary
{
    public class Account
    {
        public AccountHeader AccountHeader { get; set; }
        public List<AccountInfo> AccountInfo { get; set; }
        public List<InvoiceBalance> Balances { get; set; }

        public Account()
        {
            AccountInfo = new List<AccountInfo>();
            Balances = new List<InvoiceBalance>();
        }
    }
}
