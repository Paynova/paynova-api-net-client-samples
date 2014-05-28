using MvcSample.Core;
using MvcSample.Core.Collections;

namespace MvcSample.Models
{
    public class PartialViewNotification : INotification
    {
        public string PartialName { get; private set; }
        public DynamicDictionary Model { get; set; }

        public PartialViewNotification(string partialName, DynamicDictionary model = null)
        {
            PartialName = partialName;
            Model = model;
        }
    }
}