using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MvcSample.Core.Collections;
using MvcSample.Models;

namespace MvcSample.Core
{
    public class AppNotifications : IEnumerable<INotification>
    {
        protected List<INotification> State { get; private set; }

        public bool HasNotifications{get { return State.Any(); }}

        public AppNotifications()
        {
            State = new List<INotification>();
        }

        public virtual void RegisterViewNotification(string partialName, Action<dynamic> dataConf)
        {
            if (!partialName.StartsWith("_"))
                partialName = "_" + partialName;

            dynamic d = new DynamicDictionary();
            dataConf(d);
            State.Add(new PartialViewNotification(partialName, d));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual IEnumerator<INotification> GetEnumerator()
        {
            return State.GetEnumerator();
        }
    }
}