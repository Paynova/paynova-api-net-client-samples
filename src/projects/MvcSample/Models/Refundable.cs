namespace MvcSample.Models
{
    public class Refundable
    {
        public MetaData Meta { get; set; }
        public string TransactionId { get; set; }
        public decimal TotalAmount { get; set; }

        public class MetaData
        {
            public string OrderNumber { get; set; }
            public string PaymentMethod { get; set; }
        }
    }
}