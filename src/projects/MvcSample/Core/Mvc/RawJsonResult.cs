using System.Web.Mvc;

namespace MvcSample.Core.Mvc
{
    public class RawJsonResult : ContentResult
    {
        public RawJsonResult(string json)
        {
            Content = json;
            ContentType = "application/json";
        }
    }
}