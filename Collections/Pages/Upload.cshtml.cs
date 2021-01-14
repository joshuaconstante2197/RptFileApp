using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileProcessingLibrary;
using FileProcessingLibrary.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CollectionsWebLayer.Pages
{
    public class UploadModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UploadModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public DisplayDbData getData = new DisplayDbData();
        public void OnGet()
        {
        }
        public async Task<ActionResult> OnPost()
        {
                
                try
                {
                    var rptFileUpload = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
                    var tempFile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";

                    using (var fileStream = new FileStream(rptFileUpload, FileMode.Create))
                    {
                        await Upload.CopyToAsync(fileStream);
                    }

                    ProcessFile.Process(rptFileUpload, tempFile, _hostingEnvironment.ContentRootPath + "\\Data");
                    getData.DownloadFileToProjectFolder(_hostingEnvironment.ContentRootPath + "\\Data");
                    CurateDb.DeleteNegativeAndZeroAccounts1();
                    return new RedirectToPageResult("Index");
                }
                catch (Exception ex)
                {
                    var Err = new CreateLogFiles();
                    Err.ErrorLog(Config.WebDataPath + "err.log", ex.Message + "Error uploading file");
                    return new RedirectToPageResult("Error");
                    throw;
                }
            

        }
    }
}
