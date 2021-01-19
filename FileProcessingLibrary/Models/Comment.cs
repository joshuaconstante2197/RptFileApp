using System;
using System.Collections.Generic;
using System.Text;

namespace FileProcessingLibrary.Models
{
    public class Comment
    {
        public string ArCode { get; set; }
        public int TransactionId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentTime { get; set; }
    }
}
