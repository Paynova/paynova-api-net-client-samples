using System.Web.Mvc;

namespace MvcSample.Controllers
{
    public class StateController : Controller
    {
        [HttpPost]
        public ActionResult Clear()
        {
            MvcApplication.Store.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}