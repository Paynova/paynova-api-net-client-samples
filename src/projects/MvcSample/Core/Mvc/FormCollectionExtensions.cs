using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcSample.Core.Mvc
{
    public static class FormCollectionExtensions
    {
        public static Dictionary<string, string> ToDictionary(this FormCollection form)
        {
            return form.AllKeys
                .Select(key => new KeyValuePair<string, string>(key, form.GetValue(key).AttemptedValue))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        } 
    }
}