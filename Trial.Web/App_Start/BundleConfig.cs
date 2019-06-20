using System.Web;
using System.Web.Optimization;

namespace Trial.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/panel/style").Include(
                "~/Content/bootstrap.css",
                "~/Content/materialadmin.css",
                "~/Content/fa-solid.min.css",
                "~/Content/fa-brands.min.css",
                "~/Content/fontawesome.min.css",
                "~/Content/toastr.css",
                "~/Content/bootstrap-datetimepicker.css",
                "~/Content/jquery-ui-theme.css",
                "~/Content/select2.css",
                "~/Content/bootstrap-tagsinput.css",
                "~/Content/datatables.min.css",
                "~/Content/jquery.fancybox.min.css",
                "~/Content/main.css",
                "~/Content/datepicker3.css"
                ));

            bundles.Add(new ScriptBundle("~/panel/validate").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/panel/script").Include(
                "~/Scripts/jquery-1.11.2.min.js",
                "~/Scripts/jquery-migrate-1.2.1.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/moment-locales.min.js",
                "~/Scripts/jquery.nanoscroller.min.js",
                "~/Scripts/bootstrap-datetimepicker.js",
                "~/Scripts/bootstrap-tagsinput.min.js",
                "~/Scripts/App.js",
                "~/Scripts/AppForm.js",
                "~/Scripts/AppNavigation.js",
                "~/Scripts/handlebars.js",
                "~/Scripts/toastr.js",
                "~/Scripts/select2.min.js",
                "~/Scripts/jquery.fancybox.min.js",
                "~/Scripts/jquery.inputmask.bundle.min.js"
                ));

        }
    }
}
