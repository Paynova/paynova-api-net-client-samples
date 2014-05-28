namespace MvcSample.Models
{
    public class SimpleOrderViewModel
    {
        public SimpleOrder New { get; private set; }

        public SimpleOrderViewModel(SimpleOrder newOrder)
        {
            New = newOrder;
        }
    }
}