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
    public class TransactionModel : PageModel
    {
        public DisplayDbData getData = new DisplayDbData();
        public SaveToDb saveData = new SaveToDb();
        public InvoiceBalance invoiceBalance;
        public AccountHeader accountHeader;
        public List<Comment> comments;

        [BindProperty]
        public Comment comment { get; set; }
        public void OnGet(string arCode, string transactionId)
        {
            invoiceBalance = getData.GetInvoiceBalance(arCode, transactionId);
            accountHeader = getData.GetAccountHeaderByArCode(arCode);
            comments = getData.GetComments(arCode, transactionId);
        }
        public ActionResult OnPost(Comment comment)
        {
            if (ModelState.IsValid)
            {
                if (saveData.SaveComment(comment))
                {
                    return new RedirectToPageResult("Transaction", comment.ArCode, comment.TransactionId);
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
                return new RedirectToPageResult("Transaction", comment.ArCode, comment.TransactionId);
            }
        }
    }
}