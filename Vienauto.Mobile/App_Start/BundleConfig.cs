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
            //css 
            var coreCss = BundleConfigHelper.GetVirtualPaths(BundConfigType.Css, "bunleCoreDirectory");
            bundles.Add(new StyleBundle(AppSetting.BundleCssCorePath).Include(coreCss));

            var boostrapCss = BundleConfigHelper.GetVirtualPaths(BundConfigType.Css, "bunleBootstrapDirectory");
            bundles.Add(new StyleBundle(AppSetting.BundleCssBootstrapPath).Include(boostrapCss));

            var loginCss = BundleConfigHelper.GetVirtualPaths(BundConfigType.Css, "bunleLoginDirectory");
            bundles.Add(new StyleBundle(AppSetting.BundleCssLoginPath).Include(loginCss));

            var registerCss = BundleConfigHelper.GetVirtualPaths(BundConfigType.Css, "bunleRegisterDirectory");
            bundles.Add(new StyleBundle(AppSetting.BundleCssRegisterPath).Include(registerCss));

            //js
            var coreJs = BundleConfigHelper.GetVirtualPaths(BundConfigType.Js, "bunleCoreDirectory");
            bundles.Add(new ScriptBundle(AppSetting.BundleJsCorePath).Include(coreJs));

            var loginJs = BundleConfigHelper.GetVirtualPaths(BundConfigType.Js, "bunleLoginDirectory");
            bundles.Add(new ScriptBundle(AppSetting.BundleJsLoginPath).Include(loginJs));

            var registerJs = BundleConfigHelper.GetVirtualPaths(BundConfigType.Js, "bunleRegisterDirectory");
            bundles.Add(new ScriptBundle(AppSetting.BundleJsRegisterPath).Include(registerJs));

            BundleTable.EnableOptimizations = true;
        }
    }
}
