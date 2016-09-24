using System.Web.Mvc;
using System.Configuration;
using System.Collections.Generic;
using Vienauto.Core.Extension.Configuration;

namespace VienautoMobile.Configuration
{
    public class ConfigSection
    {
        public static List<SelectListItem> GetDropDownList(string name, string controller)
        {
            var config = ConfigurationManager.GetSection("DDLConfigSection") as ConfigSectionDDLExtension;
            foreach (ConfigItem element in config.ConfigSet)
            {
                if (element.Name == name && element.Controller == controller)
                    return new List<SelectListItem>
                    {
                        new SelectListItem { Text = element.Text, Value = element.Value }
                    };
            }
            return null;
        }
    }
}
