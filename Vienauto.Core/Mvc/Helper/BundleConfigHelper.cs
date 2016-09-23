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
                //not combine directory,rasie error
                //var directory = HostingEnvironment.MapPath(Path.Combine(defaultCssFilePath, folder));
                var directory = HostingEnvironment.MapPath(string.Concat(defaultCssFilePath, folder));
                filePaths = new DirectoryInfo(directory).GetFiles("*.css", SearchOption.TopDirectoryOnly).Select(d => "~" + string.Concat(defaultCssFilePath, folder) + "/" + d.Name).ToArray();
            }
            else if (type == BundConfigType.Js) 
            {
                //not combine directory,rasie error
                //var directory = HostingEnvironment.MapPath(Path.Combine(defaultJsFilePath, folder));
                var directory = HostingEnvironment.MapPath(string.Concat(defaultJsFilePath, folder));
                filePaths = new DirectoryInfo(directory).GetFiles("*.js", SearchOption.TopDirectoryOnly).Select(d => "~" + string.Concat(defaultJsFilePath, folder) + "/" + d.Name).ToArray();
            }

            return filePaths;
        }
    }
}
