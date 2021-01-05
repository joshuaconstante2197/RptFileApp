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
    public class AccountModel : PageModel
    {
        DisplayDbData getData = new DisplayDbData();
        public List<AccountInfo> accountInfos = new List<AccountInfo>();
        public AccountHeader accountHeader;
        
        public void OnGet(string id)
        {
            accountInfos = getData.DisplayAccountInfo(id);
            accountHeader = getData.GetAccountHeaderByArCode(id);
            foreach (var account in accountInfos)
            {
                account.TranBalance = getData.GetTranBalance(accountHeader.ArCode, account.TransactionId);
            }
        }
        
    }
}
