using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileProcessingLibrary;
using FileProcessingLibrary.Models;
using FileProcessingLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Collections.Pages
{
    public class AccountModel : PageModel
    {
        DisplayDbData getData = new DisplayDbData();
        public SaveToDb saveData = new SaveToDb();
        public List<AccountInfo> accountInfos = new List<AccountInfo>();
        public AccountHeader accountHeader;
        [BindProperty(SupportsGet = true)]
        public Comment comment { get; set; }

        public void OnGet(string id)
        {
            accountInfos = getData.DisplayAccountInfo(id);
            accountHeader = getData.GetAccountHeaderByArCode(id);
            foreach (var account in accountInfos)
            {
                account.TranBalance = getData.GetTranBalance(accountHeader.ArCode, account.TransactionId);
            }
            getData.GetLastComment(accountInfos);
        }
        public ActionResult OnPost(Comment comment)
        {
            if (ModelState.IsValid)
            {
                if (saveData.SaveComment(comment))
                {
                    return new RedirectToPageResult("Account", comment.ArCode);
                }
                else
                {
                    return new RedirectToPageResult("Error", "Error inserting comment, please try again");
                }
            }
            else
            {
                foreach (var value in ModelState.Values)
                {
                    ModelState.AddModelError("", value.ValidationState.ToString());
                }
                return new RedirectToPageResult("Account", comment.ArCode);
            }
        }


    }
}
