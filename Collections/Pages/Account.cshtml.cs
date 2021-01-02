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
        public void OnGet(string id)
        {
            accountInfos = getData.DisplayAccountInfo(id); 
        }
        
    }
}
