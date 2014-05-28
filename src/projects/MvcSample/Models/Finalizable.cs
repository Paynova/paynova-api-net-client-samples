using System;

namespace MvcSample.Models
{
    public class Finalizable
    {
        public MetaData Meta { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public Guid OrderId { get; set; }
        
        public class MetaData
        {
            public string CurrencyCode { get; set; }
            public string CardFirstSix { get; set; }
            public string CardLastFour { get; set; }     
        }
    }
}