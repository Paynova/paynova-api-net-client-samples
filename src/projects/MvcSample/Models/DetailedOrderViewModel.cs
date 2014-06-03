namespace MvcSample.Models
{
    public class DetailedOrderViewModel
    {
        public Order New { get; private set; }

        public DetailedOrderViewModel(Order order)
        {
            New = order;
        }
    }
}