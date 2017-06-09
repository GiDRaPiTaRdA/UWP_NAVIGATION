using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LoadingsManager.ParseXml
{
    public class ModuleContainer 
    {
        private readonly Dictionary<string, object> Properties;

        public ModuleContainer(Dictionary<string, object> properties = null)
        {
            this.Properties = properties ?? new Dictionary<string, object>();
        }

        public void AddPropery(string name, object value)
        {
            this.Properties.Add(name,value);
        }

        public object GetProperty(string key)
        {
            object result = this.Properties[key];
            return result;
        }
    }
}