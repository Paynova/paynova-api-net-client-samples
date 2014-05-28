namespace MvcSample.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }

        public Customer()
        {
            ShippingAddress = new Address();
            BillingAddress = new Address();
        }
    }
}