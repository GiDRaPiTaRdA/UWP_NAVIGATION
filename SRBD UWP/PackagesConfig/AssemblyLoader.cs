using System.Collections.Generic;
using System.Reflection;
using SRBD_UWP.ImportManager;

namespace SRBD_UWP.PackagesConfig
{
    public static class AssemblyLoader
    {
        private const string loadingConfigPath = @"\PackagesConfig\LoadingModulesConfig.xml";

        public static List<Assembly> getAssemblies(string path)
        {
            List<Assembly> allAssemblies = new List<Assembly>();

            XmlFileParser parser = new XmlFileParser(path + loadingConfigPath);
            var modules = parser.GetModulesFromXml();

            foreach (ModuleContainer module in modules)
            {
                string assembyName = module.GetDynamicObj().GetProperty("Name");
                Assembly assembly = Assembly.Load(new AssemblyName(assembyName));
                allAssemblies.Add(assembly);
            }

            return allAssemblies;
        }
    }
}