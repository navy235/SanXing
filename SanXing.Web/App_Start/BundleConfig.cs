using System.Web;
using System.Web.Optimization;

namespace SanXing.Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                           "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/underscore").Include(
                   "~/Scripts/underscore-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
               "~/Scripts/jquery.unobtrusive*",
               "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerycontrol").Include(
               "~/Scripts/formcontrol/control-*"
               ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                           "~/Scripts/bootstrap.js",
                           "~/Scripts/bootstrap-select.js",
                           "~/Scripts/bootstrap-modalmanager.js",
                           "~/Scripts/bootstrap-modal.js",
                           "~/Scripts/bootstrap-datepicker.js",
                           "~/Scripts/bootstrap-datepicker.zh-CN.js",
                           "~/Scripts/bootstrap-spinner.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                "~/Scripts/base/base-*"
            ));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/site.css"
                        , "~/Content/animate.css"));

            bundles.Add(new StyleBundle("~/Content/login")
                        .Include("~/Content/site.css",
                         "~/Content/animate.css",
                         "~/Content/login.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap/css")
             .Include("~/Content/bootstrap/bootstrap.css",
                      "~/Content/bootstrap/bootstrap-select.css",
                      "~/Content/bootstrap/bootstrap-modal-bs3patch.css",
                      "~/Content/bootstrap/bootstrap-modal.css",
                      "~/Content/bootstrap/bootstrap-datepicker3.css",
                      "~/Content/bootstrap/bootstrap-spinner.css"));


            bundles.Add(new StyleBundle("~/Content/kendo/2012.3.1114/css")
              .Include(
                 "~/Content/kendo/2012.3.1114/kendo.common.min.css",
                 "~/Content/kendo/2012.3.1114/kendo.dataviz.min.css",
                 "~/Content/kendo/2012.3.1114/kendo.default.min.css"
            ));


            bundles.IgnoreList.Clear();
            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        }
    }
}