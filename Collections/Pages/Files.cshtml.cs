using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileProcessingLibrary;
using FileProcessingLibrary.Models;
using FileProcessingLibrary.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CollectionsWebLayer.Pages
{
    public class FilesModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FilesModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public List<Files> files;
        public DisplayDbData getData = new DisplayDbData();
        public void OnGet()
        {
            files = getData.GetAllFiles();
        }
        public IActionResult OnGetDownload(int id)
        {
            var file = getData.DownloadFileToProjectFolder(_hostingEnvironment.ContentRootPath + @"\Data", id);
            
            return File(file, "text/plain", Path.GetFileName(file.Name));
        }
    }
}
