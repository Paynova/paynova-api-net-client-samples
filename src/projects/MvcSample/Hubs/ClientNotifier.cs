using Microsoft.AspNet.SignalR;
using MvcSample.Core;

namespace MvcSample.Hubs
{
    public class ClientNotifier : IClientNotifier
    {
        protected IHubContext Callbacks { get; private set; }

        public ClientNotifier()
        {
            Callbacks = GlobalHost.ConnectionManager.GetHubContext<CallbacksHub>();
        }

        public void NotifyCallbackCount(int count)
        {
            Callbacks.Clients.All.notifyCallbackCount(count);
        }
    }
}