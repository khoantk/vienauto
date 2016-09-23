using System.Web;
using System.Web.Optimization;
using Vienauto.Core.Extension;
using Vienauto.Core.Mvc.Helper;

namespace VienautoRemake
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/js/jquery").Include(
            //            "~/Assets/js/core/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/js/jqueryval").Include(
            //            "~/Assets/js/core/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/js/modernizr").Include( 
            //            "~/Assets/js/core/modernizr-*"));

            var coreJsVirtualPaths = BundleConfigHelper.GetVirtualPaths(BundConfigType.Js, "bunleCoreDirectory");
            bundles.Add(new ScriptBundle(ConfigExtensions<string>.GetValue("bunleJsCorePath")).Include(coreJsVirtualPaths));

            var loginJsVirtualPaths = BundleConfigHelper.GetVirtualPaths(BundConfigType.Js, "bunleLoginDirectory");
            bundles.Add(new ScriptBundle(ConfigExtensions<string>.GetValue("bunleJsLoginPath")).Include(loginJsVirtualPaths));

            var loginCssVirtualPaths = BundleConfigHelper.GetVirtualPaths(BundConfigType.Css, "bunleLoginDirectory");
            bundles.Add(new StyleBundle(ConfigExtensions<string>.GetValue("bunleCssLoginPath")).Include(loginCssVirtualPaths));

            var registerJsVirtualPaths = BundleConfigHelper.GetVirtualPaths(BundConfigType.Js, "bunleRegisterDirectory");
            bundles.Add(new ScriptBundle(ConfigExtensions<string>.GetValue("bunleJsRegisterPath")).Include(registerJsVirtualPaths));

            var registerCssVirtualPaths = BundleConfigHelper.GetVirtualPaths(BundConfigType.Css, "bunleRegisterDirectory");
            bundles.Add(new StyleBundle(ConfigExtensions<string>.GetValue("bunleCssRegisterPath")).Include(registerCssVirtualPaths));
        }
    }
}
