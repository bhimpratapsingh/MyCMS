using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ControlPanel.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Content/bower_components/jquery/dist/jquery.min.js",
                            "~/Content/bower_components/bootstrap/dist/js/bootstrap.min.js",
                            "~/Content/plugins/iCheck/icheck.min.js",
                            "~/Scripts/jquery-2.1.1.min.js",
                            "~/Scripts/notify.min.js",
                            "~/Scripts/App.js"
                            )
                        );

            bundles.Add(new StyleBundle("~/bundles/cssDesign").Include(
                            "~/Content/bower_components/bootstrap/dist/css/bootstrap.min.css",
                            "~/Content/bower_components/font-awesome/css/font-awesome.min.css",
                            "~/Content/bower_components/Ionicons/css/ionicons.min.css",
                            "~/Content/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                            "~/Content/dist/css/AdminLTE.min.css",
                            "~/Content/plugins/iCheck/square/blue.css",
                            "~/Content/CSS/StyleSheet.css"
                            )
                        );

            BundleTable.EnableOptimizations = true;
        }
    }
}