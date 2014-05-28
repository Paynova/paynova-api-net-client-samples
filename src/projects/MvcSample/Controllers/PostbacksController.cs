using System.Web.Mvc;
using MvcSample.Core;
using MvcSample.Core.Mvc;
using MvcSample.Core.Storage;
using MvcSample.Models;

namespace MvcSample.Controllers
{
    public class PostbacksController : Controller
    {
        private readonly IPostbackResultStore _postbackResultStore;

        public PostbacksController()
        {
            _postbackResultStore = MvcApplication.PostbackResultStore;
        }

        [HttpPost]
        public ActionResult Success(FormCollection form)
        {
            var postbackResult = _postbackResultStore.PutSuccessful(form.ToDictionary());

            return View(new PostbackViewModel(postbackResult));
        }

        [HttpPost]
        public ActionResult Cancel(FormCollection form)
        {
            var postbackResult = _postbackResultStore.PutCancelled(form.ToDictionary());

            return View(new PostbackViewModel(postbackResult));
        }

        [HttpPost]
        public ActionResult Pending(FormCollection form)
        {
            var postbackResult = _postbackResultStore.PutPending(form.ToDictionary());

            return View(new PostbackViewModel(postbackResult));
        }
    }
}