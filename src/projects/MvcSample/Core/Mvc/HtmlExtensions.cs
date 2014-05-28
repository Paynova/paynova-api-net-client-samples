using System;
using System.Linq.Expressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MvcSample.Core.Mvc
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString MyLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.LabelFor(expression, new { @class = "control-label" });
        }

        public static MvcHtmlString MyTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.TextBoxFor(expression, new { @class = "form-control", @readonly = "readonly" });
        }

        public static MvcHtmlString MyDataBoundTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string dataBind)
        {
            return htmlHelper.TextBoxFor(expression, new { @class = "form-control", @readonly = "readonly", data_bind = dataBind });
        }

        public static MvcHtmlString MetaAcceptLanguage<T>(this HtmlHelper<T> html)
        {
            var acceptLanguage = HttpUtility.HtmlAttributeEncode(Thread.CurrentThread.CurrentUICulture.ToString());

            return new MvcHtmlString(String.Format("<meta name=\"accept-language\" content=\"{0}\" />", acceptLanguage));
        }
    }
}