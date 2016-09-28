using Vienauto.Core.Extension;

namespace Vienauto.Mobile.Configuration
{
    public class AppSetting
    {
        public static string NhibernateConfig => ConfigExtensions<string>.GetValue("nhibernateConfig");
        public static string MailFrom => ConfigExtensions<string>.GetValue("mailFrom");
        public static string MailTo => ConfigExtensions<string>.GetValue("mailTo");
        public static string[] Cc => ConfigExtensions<string[]>.GetValue("cc");
        public static string BundleCssCorePath => ConfigExtensions<string>.GetValue("bunleCssCorePath");
        public static string BundleCssBootstrapPath => ConfigExtensions<string>.GetValue("bunleCssBootstrapPath");
        public static string BundleCssLoginPath => ConfigExtensions<string>.GetValue("bunleCssLoginPath");
        public static string BundleCssRegisterPath => ConfigExtensions<string>.GetValue("bunleCssRegisterPath");
        public static string BundleJsCorePath => ConfigExtensions<string>.GetValue("bunleJsCorePath");
        public static string BundleJsLoginPath => ConfigExtensions<string>.GetValue("bunleJsLoginPath");
        public static string BundleJsRegisterPath => ConfigExtensions<string>.GetValue("bunleJsRegisterPath");
    }
}
