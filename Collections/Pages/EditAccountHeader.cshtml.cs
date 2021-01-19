using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileProcessingLibrary;
using FileProcessingLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CollectionsWebLayer.Pages
{
    public class EditAccountHeaderModel : PageModel
    {
        
        public SaveToDb saveToDb = new SaveToDb();
        public DisplayDbData displayDbData = new DisplayDbData();
        [BindProperty(SupportsGet = true)]
        public AccountHeader accountHeader { get; set; }
        public void OnGet(string arCode)
        {
            accountHeader = displayDbData.GetAccountHeaderByArCode(arCode);
        }
        public ActionResult OnPost(AccountHeader accountHeader)
        {
            if (ModelState.IsValid)
            {
                if (saveToDb.EditAccountHeader(accountHeader))
                {
                    return new RedirectToPageResult("Account", new { id = accountHeader.ArCode });
                }
                else
                {
                    return new RedirectToPageResult("Error","Unsusccesfull account information update");
                }
            }
            else
            {
                return Page();
            }
        }
    }
}