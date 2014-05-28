using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MvcSample.Core;
using MvcSample.Core.Mvc;
using MvcSample.Core.Serialization;
using MvcSample.Core.Storage;
using MvcSample.Hubs;

namespace MvcSample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        [ThreadStatic]
        private static AppNotifications _notifications;

        public static ISerializer JsonSerializer { get; private set; }
        public static IStore Store { get; private set; }
        public static ICallbackResultStore CallbackResultStore { get; private set; }
        public static IPostbackResultStore PostbackResultStore { get; private set; }
        public static IClientNotifier ClientNotifier { get; private set; }
        public static AppNotifications Notifications { get { return _notifications; } }

        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelMetadataProviders.Current = new HumanizerMetadataProvider();

            JsonSerializer = new JsonSerializer();
            Store = new InMemoryStore();
            DataSeeder.Seed(Store);

            CallbackResultStore = new CallbackResultStore(Store);
            PostbackResultStore = new PostbackResultStore(Store);
            ClientNotifier = new ClientNotifier();
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            _notifications = new AppNotifications();

            CheckIfCallbackServerEndpointMatchesCurrentHost();
        }

        private void CheckIfCallbackServerEndpointMatchesCurrentHost()
        {
            if (AppSettings.CallbacksServerEndpoint.Authority != Request.Url.Authority)
                Notifications.RegisterViewNotification("CallbacksServerEndpoint", d =>
                {
                    d.CallbacksAbsoluteUri = AppSettings.CallbacksServerEndpoint.AbsoluteUri;
                    d.CurrentAbsoluteUri = Request.Url.AbsoluteUri;
                });
        }
    }
}