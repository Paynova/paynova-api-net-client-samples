using System;
using System.Web.Mvc;
using MvcSample.Core;
using MvcSample.Core.Storage;
using MvcSample.Models;
using MvcSample.Services;
using Paynova.Api.Client;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Responses;

namespace MvcSample.Controllers
{
    public class SamplesController : Controller
    {
        private readonly ICallbackResultStore _callbackResultStore;

        public SamplesController()
        {
            _callbackResultStore = MvcApplication.CallbackResultStore;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        private SamplePaynovaService GetPaynovaService()
        {
            var client = new PaynovaClient(AppSettings.PaynovaServerUrl, AppSettings.PaynovaUser, AppSettings.PaynovaPassword);

            return new SamplePaynovaService(client, _callbackResultStore);
        }

        [HttpGet]
        public ActionResult SimpleOrder()
        {
            var uniqueSampleOrderNumber = Guid.NewGuid().ToString("n");
            var newSampleOrder = new SimpleOrder
            {
                CurrencyCode = CurrencyCode.SwedishKrona.Alpha3,
                OrderNumber = uniqueSampleOrderNumber,
                TotalAmount = 100
            };
            var viewModel = new SimpleOrderViewModel(newSampleOrder);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PaynovaClientExceptionToTempData("~/samples/simpleorder")]
        public ActionResult SimpleOrder(SimpleOrder model)
        {
            if (!ModelState.IsValid)
                return View(new SimpleOrderViewModel(model));

            var service = GetPaynovaService();

            var urlForPaymentUi = service.CreateOrder(model.OrderNumber, model.CurrencyCode, model.TotalAmount);

            //Since no PaynovaClientException has been thrown, we can redirect our customer
            //to the URL found in the response.
            return Redirect(urlForPaymentUi);
        }

        [HttpGet]
        public ActionResult DetailedOrder()
        {
            var uniqueSampleOrderNumber = Guid.NewGuid().ToString("n");
            var newSampleOrder = new Order
            {
                OrderNumber = uniqueSampleOrderNumber,
                CurrencyCode = CurrencyCode.SwedishKrona.Alpha3
            };
            newSampleOrder.AddLine("ts002", "Green t-shirt", 1, 50m);
            newSampleOrder.AddLine("ts002", "Blue t-shirt", 2, 80m);

            var viewModel = new DetailedOrderViewModel(newSampleOrder);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PaynovaClientExceptionToTempData("~/samples/detailedorder")]
        public ActionResult DetailedOrder(Order model)
        {
            if (!ModelState.IsValid)
                return View(new DetailedOrderViewModel(model));

            var service = GetPaynovaService();

            var urlForPaymentUi = service.CreateOrder(model);

            //Since no PaynovaClientException has been thrown, we can redirect our customer
            //to the URL found in the response.
            return Redirect(urlForPaymentUi);
        }

        [HttpGet]
        public ActionResult Finalize()
        {
            var viewModel = new FinalizeViewModel
            {
                Finalized = GetFromTempdata<FinalizeAuthorizationResponse>()
            }
            .SetFinalizable(_callbackResultStore.GetFinalizablePayments());

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PaynovaClientExceptionToTempData("~/samples/finalize")]
        public ActionResult Finalize(Finalizable model)
        {
            if (!ModelState.IsValid)
                return View(new FinalizeViewModel().SetFinalizable(_callbackResultStore.GetFinalizablePayments()));

            var service = GetPaynovaService();

            var response = service.Finalize(model.TransactionId, model.OrderId, model.Amount);

            //For demo. Add to tempdata so that we can extract data from it in the view
            //and show successful notification
            TempData.Add(typeof(FinalizeAuthorizationResponse).Name, response);

            //Since no PaynovaClientException has been thrown, we can redirect our customer
            return RedirectToAction("Finalize");
        }

        private T GetFromTempdata<T>() where T : class
        {
            return TempData.ContainsKey(typeof(T).Name)
                ? (T)TempData[typeof(T).Name]
                : null;
        }

        [HttpGet]
        public ActionResult Refund()
        {
            return View(new RefundViewModel
            {
                Refunded = GetFromTempdata<RefundPaymentResponse>()
            }
            .SetRefundable(_callbackResultStore.GetRefundablePayments()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PaynovaClientExceptionToTempData("~/samples/refund")]
        public ActionResult Refund(Refundable model)
        {
            if (!ModelState.IsValid)
                return View("Refund", new RefundViewModel().SetRefundable(_callbackResultStore.GetRefundablePayments()));

            var service = GetPaynovaService();

            var response = service.Refund(model.TransactionId, model.TotalAmount);

            //For demo. Add to tempdata so that we can extract data from it in the view
            //and show successful notification
            TempData.Add(typeof(RefundPaymentResponse).Name, response);

            //Since no PaynovaClientException has been thrown, we can redirect our customer
            return RedirectToAction("Refund");
        }
    }
}