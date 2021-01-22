using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileProcessingLibrary;
using FileProcessingLibrary.Models;
using FileProcessingLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CollectionsWebLayer.Pages
{
    public class MetricsModel : PageModel
    {
        public DisplayDbData displayDbData = new DisplayDbData();
        public List<TopAccount> topAccounts;
        public TotalAR totalAr;
        public void OnGet()
        {
            topAccounts = displayDbData.GetTopAccounts(10);
            totalAr = displayDbData.GetTotalBalance();
            decimal total = topAccounts.Sum(x => x.Balance);
            foreach (var topAccount in topAccounts)
            {
                topAccount.Percentage = (topAccount.Balance * 100) / totalAr.Balance;
            }

        }
    }
}
