using System.Linq;
using System.Net;
using System.Web.Mvc;
using MvcSample.Core;
using MvcSample.Core.Mvc;
using MvcSample.Core.Storage;
using MvcSample.Models;

namespace MvcSample.Controllers
{
    public class CallbacksController : Controller
    {
        private readonly ICallbackResultStore _callbackResultStore;
        private readonly IClientNotifier _clientNotifier;

        public CallbacksController()
        {
            _callbackResultStore = MvcApplication.CallbackResultStore;
            _clientNotifier = MvcApplication.ClientNotifier;
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            //Just a temporary store for demo purposes
            //so that we can expose callback results in a sample view.
            _callbackResultStore.Put(form.ToDictionary());

            //Simple solution using SignalR to push updates to clients.
            _clientNotifier.NotifyCallbackCount(_callbackResultStore.Count);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new CallbacksViewModel(_callbackResultStore.GetAll().ToArray());

            return View(viewModel);
        }

        [HttpGet]
        public int Count()
        {
            return _callbackResultStore.Count;
        }
    }
}