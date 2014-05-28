using System.Collections.Generic;
using MvcSample.Core.Storage;
using MvcSample.Models;

namespace MvcSample
{
    public class DataSeeder
    {
        public static void Seed(IStore store)
        {
            foreach (var customer in CreateSampleCustomers())
            {
                store.Put(customer.Id, customer);
            }
        }

        private static IEnumerable<Customer> CreateSampleCustomers()
        {
            yield return new Customer
            {
                Id = "1",
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@paynova.com",
                ShippingAddress = new Models.Address
                {
                    Street = "The shipping street 1",
                    City = "Stockholm",
                    Zip = "100 00",
                    CountryCode = "SWE"
                },
                BillingAddress = new Models.Address
                {
                    Street = "The billing street 1",
                    City = "Gothenburg",
                    Zip = "400 00",
                    CountryCode = "SWE"
                }
            };

            yield return new Customer
            {
                Id = "2",
                Firstname = "Forest",
                Lastname = "Gump",
                Email = "forest.gump@paynova.com",
                ShippingAddress = new Models.Address
                {
                    Street = "Fruktvägen 15",
                    City = "Stockholm",
                    Zip = "100 01",
                    CountryCode = "SWE"
                },
                BillingAddress = new Models.Address
                {
                    Street = "Fruktvägen 16",
                    City = "Gothenburg",
                    Zip = "100 01",
                    CountryCode = "SWE"
                }
            };
        } 
    }
}