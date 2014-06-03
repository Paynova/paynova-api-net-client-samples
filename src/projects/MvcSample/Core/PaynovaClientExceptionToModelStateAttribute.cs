using System.Web.Mvc;
using Paynova.Api.Client;

namespace MvcSample.Core
{
    public class PaynovaClientExceptionToModelStateAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var pex = filterContext.Exception as PaynovaSdkException;
            if (pex == null)
            {
                base.OnException(filterContext);
                return;
            }

            filterContext.ExceptionHandled = true;

            var modelState = filterContext.Controller.ViewData.ModelState;

            modelState.AddModelError(string.Empty, pex.StatusMessage);

            foreach (var error in pex.Errors)
                modelState.AddModelError(error.FieldName, error.Message);

            filterContext.Result = new ViewResult
            {
                ViewName = View,
                MasterName = Master,
                ViewData = filterContext.Controller.ViewData,
                TempData = filterContext.Controller.TempData
            };
        }
    }
}