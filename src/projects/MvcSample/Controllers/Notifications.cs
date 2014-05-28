using System.Linq;
using System.Web.Mvc;
using MvcSample.Models;

namespace MvcSample.Controllers
{
    public class NotificationsController : Controller
    {
        public PartialViewResult Index()
        {
            return PartialView(new NotificationsViewModel
            {
                Notifications = MvcApplication.Notifications.ToArray()
            });
        }
    }
}