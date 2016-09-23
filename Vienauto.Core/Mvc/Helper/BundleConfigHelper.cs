using System.IO;
using System.Linq;
using System.Web.Hosting;
using Vienauto.Core.Extension;

namespace Vienauto.Core.Mvc.Helper
{
    public enum BundConfigType
    {
        Css,
        Js
    }

    public class BundleConfigHelper
    {
        private const string defaultJsFilePath = "/Assets/js";
        private const string defaultCssFilePath = "/Assets/css";

        public static string[] GetVirtualPaths(BundConfigType type, string folderName)
        {
            string[] filePaths = null;
            var folder = ConfigExtensions<string>.GetValue(folderName);

            if (type == BundConfigType.Css)
            {
                var directory = HostingEnvironment.MapPath(Path.Combine(defaultCssFilePath, folder));
                filePaths = new DirectoryInfo(directory).GetFiles("*.css", SearchOption.TopDirectoryOnly).Select(d => d.Name).ToArray();
            }
            else if (type == BundConfigType.Js)
            {
                var directory = HostingEnvironment.MapPath(Path.Combine(defaultJsFilePath, folder));
                filePaths = new DirectoryInfo(directory).GetFiles("*.js", SearchOption.TopDirectoryOnly).Select(d => d.Name).ToArray();
            }

            return filePaths;
        }
    }
}
