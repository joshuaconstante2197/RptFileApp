using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void OnGet()
        {
            topAccounts = displayDbData.GetTopAccounts(10);
            decimal total = topAccounts.Sum(x => x.Balance);
            foreach (var topAccount in topAccounts)
            {
                topAccount.Percentage = (topAccount.Balance * 100) / total;
            }
        }
    }
}
