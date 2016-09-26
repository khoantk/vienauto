using System;
using System.Web.Mvc;
using System.Configuration;
using System.Collections.Generic;
using Vienauto.Core.Extension.Configuration;
using ObjectCache = Vienauto.Core.Data.Caching;

namespace Vienauto.Mobile.Configuration
{
    public class ConfigSection
    {
        public static List<SelectListItem> GetDropDownList(string name, string controller, string defaultOption = "")
        {
            ObjectCache.ICacheProvider<List<SelectListItem>> cacheProvider = new ObjectCache.CacheProvider<List<SelectListItem>>();
            Func<List<SelectListItem>> dataFromConfig = () => 
            {
                var selectList = new List<SelectListItem>();
                var config = ConfigurationManager.GetSection("DDLConfigSection") as ConfigSectionDDLExtension;

                if (defaultOption != "")
                    selectList.Add(new SelectListItem { Text = defaultOption, Value = "0" });

                foreach (ConfigItem element in config.ConfigSet)
                {
                    if (element.Name == name && element.Controller == controller)
                        selectList.Add(new SelectListItem { Text = element.Text, Value = element.Value });
                }

                return selectList;
            };

            var cacheKey = string.Join("-", new string[] { name, controller });
            var dataCached = cacheProvider.Fetch(cacheKey, dataFromConfig, null, TimeSpan.FromHours(4));
            return dataCached;
        }
    }
}
