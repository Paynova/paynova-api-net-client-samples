using System;
using System.Linq;
using MvcSample.Core;
using MvcSample.Core.Storage;
using MvcSample.Models;
using Paynova.Api.Client;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Responses;

namespace MvcSample.Services
{
    /// <summary>
    /// Encapsulated demo service that interacts
    /// with Paynova's API using the .Net API Client.
    /// </summary>
    public class SamplePaynovaService
    {
        protected readonly IPaynovaClient Client;
        protected readonly ICallbackResultStore CallbackResultStore;

        public SamplePaynovaService(IPaynovaClient client, ICallbackResultStore callbackResultStore)
        {
            Client = client;
            CallbackResultStore = callbackResultStore;
        }

        public virtual string CreateOrder(string orderNumber, string currencyCode, decimal totalAmount)
        {
            //First we need to create the order
            var createOrderResponse = Client.CreateOrder(orderNumber, currencyCode, totalAmount);

            //Now that we have our order created, the next step is to initialize a payment,
            var initPaymentRequest = CreateInitializePaymentRequest(createOrderResponse.OrderId, orderNumber, totalAmount);
            var initPaymentResponse = Client.InitializePayment(initPaymentRequest);

            return initPaymentResponse.Url;
        }

        public virtual string CreateOrder(Order order)
        {
            //First we need to create the order
            var lineItems = order.OrderLines.Select((line, index) => new LineItem(index, line.ArticleNumber, line.ArticleName, "st", line.VatPercent, line.Quantity, line.UnitPrice, line.TotalAmount, line.TotalVatAmount));
            var createOrderRequest = new CreateOrderRequest(order.OrderNumber, order.CurrencyCode, order.TotalAmount)
                .WithLineItems(lineItems)
                .WithCustomer(c =>
                {
                    c.CustomerId = order.Customer.Id;
                    c.EmailAddress = order.Customer.Email;
                    c.Name.FirstName = order.Customer.Firstname;
                    c.Name.LastName = order.Customer.Lastname;
                })
                .WithBillTo(a =>
                {
                    a.Address.Street1 = order.Customer.BillingAddress.Street;
                    a.Address.PostalCode = order.Customer.BillingAddress.Zip;
                    a.Address.City = order.Customer.BillingAddress.City;
                    a.Address.CountryCode = order.Customer.BillingAddress.CountryCode;
                })
                .WithShipTo(a =>
                {
                    a.Address.Street1 = order.Customer.ShippingAddress.Street;
                    a.Address.PostalCode = order.Customer.ShippingAddress.Zip;
                    a.Address.City = order.Customer.ShippingAddress.City;
                    a.Address.CountryCode = order.Customer.ShippingAddress.CountryCode;
                });

            var createOrderResponse = Client.CreateOrder(createOrderRequest);

            //Now that we have our order created, the next step is to initialize a payment,
            var initPaymentRequest = CreateInitializePaymentRequest(createOrderRequest, createOrderResponse);
            var initPaymentResponse = Client.InitializePayment(initPaymentRequest);

            return initPaymentResponse.Url;
        }

        public virtual FinalizeAuthorizationResponse Finalize(string paynovaTransactionId, Guid paynovaOrderId, decimal amountToFinalize)
        {
            var request = new FinalizeAuthorizationRequest(paynovaTransactionId, paynovaOrderId, amountToFinalize);

            var response = Client.FinalizeAuthorization(request);

            CallbackResultStore.MarkAsFinalized(request.TransactionId);

            return response;
        }

        public virtual RefundPaymentResponse Refund(string paynovaTransactionId, decimal totalAmount)
        {
            var request = new RefundPaymentRequest(paynovaTransactionId, totalAmount);

            var response = Client.RefundPayment(request);

            CallbackResultStore.MarkAsRefunded(request.TransactionId);

            return response;
        }

        protected virtual InitializePaymentRequest CreateInitializePaymentRequest(CreateOrderRequest orderRequest, CreateOrderResponse orderResponse)
        {
            var request = CreateInitializePaymentRequest(orderResponse.OrderId, orderRequest.OrderNumber, orderRequest.TotalAmount)
                .WithLineItems(orderRequest.LineItems);

            request.InterfaceOptions.DisplayLineItems = orderRequest.LineItems.Any();

            //TODO: For card payments
            //var allowStoreOfProfileCard = orderRequest.Customer != null && !string.IsNullOrWhiteSpace(orderRequest.Customer.CustomerId);
            //if (allowStoreOfProfileCard)
            //{
            //    request.ProfilePaymentOptions = new ProfilePaymentOptions(orderRequest.Customer.CustomerId)
            //    {
            //        DisplaySaveProfileCardOption = true
            //    };
            //}

            return request;
        }

        protected virtual InitializePaymentRequest CreateInitializePaymentRequest(Guid paynovaOrderId, string orderNumber, decimal totalAmount)
        {
            //Now that we have our order created, the next step is to initialize a payment.
            //This requires us to e.g. provide some URLs where Paynova should redirect the customer
            //upon success, cancel and pending, which is done via the interface options.
            var interfaceOptions = GetDefaultInterfaceOptions();
            var paymentMethods = GetDefaultPaymentMethods();

            //We can also specify the different payment methods, layouts etc.
            return new InitializePaymentRequest(paynovaOrderId, totalAmount, PaymentChannelId.Web, interfaceOptions)
                .WithPaymentMethods(paymentMethods);
        }

        protected virtual PaymentMethod[] GetDefaultPaymentMethods()
        {
            return new[] { PaymentMethod.Visa, PaymentMethod.MasterCard, PaymentMethod.NordeaSweden };
        }

        protected virtual InterfaceOptions GetDefaultInterfaceOptions()
        {
            var callbacksServerEndpoint = AppSettings.CallbacksServerEndpoint;

            var paymentInterfaceOptions = new InterfaceOptions(
                InterfaceId.Aero,
                customerLanguageCode: "ENG",
                urlRedirectSuccess: new Uri(callbacksServerEndpoint, "/postbacks/success"),
                urlRedirectCancel: new Uri(callbacksServerEndpoint, "/postbacks/cancel"),
                urlRedirectPending: new Uri(callbacksServerEndpoint, "/postbacks/pending"))
            {
                //You can also provide a callback URL, to which Paynova
                //will POST details about the payment.
                UrlCallback = new Uri(callbacksServerEndpoint, "/callbacks")
            };
            return paymentInterfaceOptions;
        }
    }
}