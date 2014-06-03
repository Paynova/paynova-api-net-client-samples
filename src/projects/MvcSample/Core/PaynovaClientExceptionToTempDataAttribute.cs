using System;
using System.Web.Mvc;
using Paynova.Api.Client;

namespace MvcSample.Core
{
    public class PaynovaClientExceptionToTempDataAttribute : HandleErrorAttribute
    {
        public string RedirectTo { get; private set; }

        public PaynovaClientExceptionToTempDataAttribute(string redirectTo)
        {
            RedirectTo = redirectTo;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            var pex = filterContext.Exception as PaynovaSdkException;
            if (pex == null)
            {
                base.OnException(filterContext);
                return;
            }
            filterContext.ExceptionHandled = true;
            filterContext.Controller.TempData.Add(pex.GetType().Name + "_" + Guid.NewGuid().ToString("n"), filterContext.Exception);
            filterContext.Result = new RedirectResult(RedirectTo);
        }
    }
}