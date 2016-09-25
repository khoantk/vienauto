using System.Web.Mvc;
using System.Configuration;
using System.Collections.Generic;
using Vienauto.Core.Extension.Configuration;

namespace Vienauto.Mobile.Configuration
{
    public class ConfigSection
    {
        public static List<SelectListItem> GetDropDownList(string name, string controller, string defaultOption = "")
        {
            var selectList = new List<SelectListItem>();
            var config = ConfigurationManager.GetSection("DDLConfigSection") as ConfigSectionDDLExtension;

            if(defaultOption != "")
                selectList.Add(new SelectListItem { Text = defaultOption, Value = "0" });

            foreach (ConfigItem element in config.ConfigSet)
            {
                if (element.Name == name && element.Controller == controller)
                    selectList.Add(new SelectListItem { Text = element.Text, Value = element.Value });
            }
            return selectList;
        }
    }
}
