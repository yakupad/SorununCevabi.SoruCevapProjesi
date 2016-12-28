using System.Web;
using System.Web.Optimization;

namespace SorununCevabi
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
                      "~/Scripts/app.min.js",            
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",      
                      "~/Scripts/dashboard.js",
                      "~/Scripts/demo.js",
                      "~/Scripts/jquery.slimscroll.min.js",
                      "~/Scripts/bootstrap3-wysihtml5.all.min.js",
                      "~/Scripts/fastclick.js",
                      "~/Scripts/bootstrapValidator.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/AdminLTE.min.css",
                      "~/Content/_all-skins.min.css",
                      "~/Content/blue.css",
                      "~/Content/morris.css",
                      "~/Content/jquery-jvectormap-1.2.2.css",
                      "~/Content/datapicker3.css",
                      "~/Content/datarangepicker.css",
                      "~/Content/bootstrap3-wysihtml5.min.css",
                      "~/Content/fullcalendar.min.css",
                      "~/Content/bootstrapValidator.css",
                      "~/Content/fullcalendar.print.css"
                      ));
        }
    }
}
