using System;
using System.Collections.Generic;
using System.Text;

namespace FileProcessingLibrary.Models
{
    public class TotalAR
    {
        public int TotalId { get; set; }
        public DateTime UploadDate { get; set; }
        public decimal Balance { get; set; }
        public decimal Curr { get; set; }
        public decimal Over30 { get; set; }
        public decimal Over60 { get; set; }
        public decimal Over90 { get; set; }
    }
}
