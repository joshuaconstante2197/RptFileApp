using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileProcessingLibrary;
using FileProcessingLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Collections.Pages
{
    public class TransactionModel : PageModel
    {
        public DisplayDbData getData = new DisplayDbData();
        public InvoiceBalance invoiceBalance;
        public AccountHeader accountHeader;
        public void OnGet(string arCode, string transactionId)
        {
            invoiceBalance = getData.GetInvoiceBalance(arCode, transactionId);
            accountHeader = getData.GetAccountHeaderByArCode(arCode);
        }
    }
}