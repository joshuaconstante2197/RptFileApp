using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileProcessingLibrary;
using FileProcessingLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Collections.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public DisplayDbData GetDbData = new DisplayDbData();
        public List<AccountHeader> Accounts;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

        }

        public void OnGet()
        {
            try
            {
                //CurateDb.DeleteNegativeAndZeroAccounts();
                Accounts = GetDbData.DisplayAllAccounts();
                foreach (var account in Accounts)
                {
                    account.TotalBalance = GetDbData.GetTotalBalance(account.ArCode);
                }
            }
            catch (Exception ex)
            {
                var Err = new CreateLogFiles();
                Err.ErrorLog(Config.WebDataPath + "err.log", ex.Message + "Error on idex page");
                throw;
            }
            
        }
    }
}
