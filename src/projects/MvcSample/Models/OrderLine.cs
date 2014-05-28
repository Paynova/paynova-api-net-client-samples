namespace MvcSample.Models
{
    public class OrderLine
    {
        public string ArticleNumber { get; set; }
        public string ArticleName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal VatPercent { get; set; }
        
        public decimal TotalAmount
        {
            get { return (UnitPrice * Quantity) + TotalVatAmount; }
        }

        public decimal TotalVatAmount
        {
            get { return (UnitPrice * Quantity) * (VatPercent / 100); }
        }
    }
}