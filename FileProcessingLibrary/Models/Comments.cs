using System;
using System.Collections.Generic;
using System.Text;

namespace FileProcessingLibrary.Models
{
    public class Comments
    {
        public string ArCode { get; set; }
        public int TransactionId { get; set; }
        public string Comment { get; set; }
        public DateTime CommentTime { get; set; }
    }
}
