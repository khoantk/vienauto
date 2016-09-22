using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vienauto.Core.Extension.Configuration
{
    public class ConfigSectionDDLExtension : ConfigurationSection
    {
        private const string CONFIG_SET = "DDLSet";
        private const string CONFIG_ITEM = "DDLItem";
        private const string CONFIG_SECTION = "DDLConfigSection";
        
        [ConfigurationProperty(CONFIG_SET)]
        [ConfigurationCollection(typeof(ConfigItemCollection), AddItemName = CONFIG_ITEM)]
        public ConfigItemCollection ConfigSet
        {
            get
            {
                return (ConfigItemCollection)base[CONFIG_SET];
            }
        }

        public static ConfigSectionDDLExtension GetSection()
        {
            try
            {
                var section = ConfigurationManager.GetSection(CONFIG_SECTION) as ConfigSectionDDLExtension;
                return section;
            }
            catch (Exception ex)
            {
                //TO DO: Implement logging code here.
            }
            return null;
        }
    }

    public class ConfigItemCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigItem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var e = element as ConfigItem;
            if (e != null)
                return e.Text;
            return string.Empty;
        }
    }

    public class ConfigItem : ConfigurationElement
    {
        [ConfigurationProperty("text", IsKey = true, IsRequired = true)]
        public string Text
        {
            get { return (string)this["text"]; }
            set { this["text"] = value; }
        }

        [ConfigurationProperty("value", IsKey = true, IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }

        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("controller", IsKey = true, IsRequired = true)]
        public string Controller
        {
            get { return (string)this["controller"]; }
            set { this["controller"] = value; }
        }
    }
}
