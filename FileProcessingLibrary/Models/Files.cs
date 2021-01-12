using System;
using System.Collections.Generic;
using System.Text;

namespace FileProcessingLibrary.Models
{
    public class Files
    {
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public DateTime CreatedOn { get; set; }
        public string TypeOfFile { get; set; }
    }
}
