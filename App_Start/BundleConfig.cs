using System.Web.Optimization;

namespace MongoDB.DotNet
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/template").Include(
                      "~/Content/Template/js/jquery.min.js",
                      "~/Content/Template/js/popper.min.js",
                      "~/Content/Template/js/bootstrap.min.js",
                      "~/Content/Template/js/jquery.slimscroll.js",
                      "~/Content/Template/js/sidebarmenu.js",
                      "~/Content/Template/js/sticky-kit.min.js",
                      "~/Content/Template/js/custom.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/css/template").Include(
                       "~/Content/Template/css/bootstrap.min.css",
                       "~/Content/Template/css/helper.css",
                       "~/Content/Template/css/style.css"));
        }
    }
}
