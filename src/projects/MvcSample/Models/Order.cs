using System.Collections.Generic;
using System.Linq;

namespace MvcSample.Models
{
    public class Order
    {
        public string OrderNumber { get; set; }
        public string CurrencyCode { get; set; }
        public Customer Customer { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public decimal TotalAmount
        {
            get { return OrderLines.Sum(l => l.TotalAmount); }
        }

        public Order()
        {
            Customer = new Customer();
            OrderLines = new List<OrderLine>();
        }

        public void AddLine(string articleNumber, string articleName, int quantity, decimal price)
        {
            OrderLines.Add(new OrderLine
            {
                ArticleNumber = articleNumber,
                ArticleName = articleName,
                Quantity = quantity,
                UnitPrice = price,
                VatPercent = 25
            });
        }
    }
}