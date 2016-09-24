using System.Web.Optimization;
using Vienauto.Core.Mvc.Helper;
using Vienauto.Mobile.Configuration;

namespace VienautoRemake
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var coreJs = BundleConfigHelper.GetVirtualPaths(BundConfigType.Js, "bunleCoreDirectory"); 
            bundles.Add(new ScriptBundle(AppSetting.BundleJsCorePath).Include(coreJs));

            var coreCss = BundleConfigHelper.GetVirtualPaths(BundConfigType.Css, "bunleCoreDirectory");
            bundles.Add(new ScriptBundle(AppSetting.BundleCssCorePath).Include(coreCss));

            var loginJs = BundleConfigHelper.GetVirtualPaths(BundConfigType.Js, "bunleLoginDirectory");
            bundles.Add(new ScriptBundle(AppSetting.BundleJsLoginPath).Include(loginJs));

            var loginCss = BundleConfigHelper.GetVirtualPaths(BundConfigType.Css, "bunleLoginDirectory");
            bundles.Add(new StyleBundle(AppSetting.BundleCssLoginPath).Include(loginCss));

            var registerJs = BundleConfigHelper.GetVirtualPaths(BundConfigType.Js, "bunleRegisterDirectory");
            bundles.Add(new ScriptBundle(AppSetting.BundleJsRegisterPath).Include(registerJs));

            var registerCss = BundleConfigHelper.GetVirtualPaths(BundConfigType.Css, "bunleRegisterDirectory");
            bundles.Add(new StyleBundle(AppSetting.BundleCssRegisterPath).Include(registerCss));
        }
    }
}
