using System;
using System.Configuration;

namespace MvcSample.Core
{
    public static class AppSettings
    {
        private static readonly Lazy<Uri> CallbacksServerEndpointFn = new Lazy<Uri>(() => new Uri(Read("callbacks_server_endpoint")));
        private static readonly Lazy<string> PaynovaServerUrlFn = new Lazy<string>(() => Read("paynova_client_serverurl"));
        private static readonly Lazy<string> PaynovaUserFn = new Lazy<string>(() => Read("paynova_client_username"));
        private static readonly Lazy<string> PaynovaPasswordFn = new Lazy<string>(() => Read("paynova_client_password"));

        public static Uri CallbacksServerEndpoint { get { return CallbacksServerEndpointFn.Value; } }
        public static string PaynovaServerUrl { get { return PaynovaServerUrlFn.Value; } }
        public static string PaynovaUser { get { return PaynovaUserFn.Value; } }
        public static string PaynovaPassword { get { return PaynovaPasswordFn.Value; } }

        private static string Read(string key)
        {
            var setting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(setting))
                throw new ConfigurationErrorsException(string.Format("AppSetting: '{0}' is missing value", key));

            return setting;
        }
    }
}