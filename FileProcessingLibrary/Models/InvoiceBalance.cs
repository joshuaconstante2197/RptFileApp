namespace FileProcessingLibrary
{
    public class InvoiceBalance
    {
        public decimal Balance { get; set; }
        public decimal Current { get; set; }
        public decimal Over30 { get; set; }
        public decimal Over60 { get; set; }
        public decimal Over90 { get; set; }
    }
}
