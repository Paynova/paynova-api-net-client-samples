namespace MvcSample.Models
{
    public class SimpleOrder
    {
        public string OrderNumber { get; set; }
        public string CurrencyCode { get; set; }
        public decimal TotalAmount { get; set; }
    }
}