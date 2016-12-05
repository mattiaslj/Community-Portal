using System.Web.Optimization;

namespace CommunityPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js",
                        "~/Scripts/angular-ui/ui-bootstrap-tpls.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angularScripts").Include(
                    "~/SiteScripts/app.js",
                    "~/SiteScripts/newsFeedController.js",
                    "~/SiteScripts/forumController.js",
                    "~/SiteScripts/forumThread.js",
                    "~/SiteScripts/authentication.js",
                    "~/SiteScripts/forumPost.js",
                    "~/SiteScripts/threadController.js",
                    "~/SiteScripts/userPageController.js",
                    "~/SiteScripts/chat.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/simplex-bootstrap.min.css",
                      "~/Content/site.css"));
        }
    }
}
