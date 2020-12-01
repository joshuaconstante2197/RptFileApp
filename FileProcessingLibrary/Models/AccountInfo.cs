using System;
using System.Collections.Generic;

namespace FileProcessingLibrary
{
    public class AccountInfo
    {
        public DateTime TranDate { get; set; }
        public string TranDetail { get; set; }
        public DateTime DueDate { get; set; }
        public string InvoiceNumber { get; set; }
        public InvoiceBalance InvoiceBalances { get; set; }
        public string ReferenceNumber { get; set; }

    }
}
