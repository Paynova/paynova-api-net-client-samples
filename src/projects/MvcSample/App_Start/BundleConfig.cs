using System.Web.Optimization;

namespace MvcSample
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate/jquery.validate*",
                        "~/Scripts/jquery.validate/_extensions.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                        "~/Scripts/knockout/knockout.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                        "~/Scripts/signalr/jquery.signalR-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/app/app.js",
                        "~/Scripts/app/app.vms.*",
                        "~/Scripts/app/app.init.js"));

            bundles.Add(new StyleBundle("~/Content/styling")
                //.Include("~/Content/bootstrap/bootstrap.min.css", new CssRewriteUrlTransform())
                //.Include("~/Content/bootstrap/bootstrap-theme.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/bootstrap/bootstrap-cerulean.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/font-awesome/font-awesome.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/site.css"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}