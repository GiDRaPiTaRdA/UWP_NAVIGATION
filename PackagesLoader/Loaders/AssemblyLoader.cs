using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LoadingsManager.ParseXml;

namespace LoadingsManager.Loaders
{
    public class AssemblyLoader: Loader
    {
        public string FullDirectoryPath => Directory.GetCurrentDirectory() +"/"+ this.ConfigFilePath;

        public AssemblyLoader(string localDirecroryPath) : base(localDirecroryPath) { }

        public List<Assembly> getAssemblies()
        {
            List<Assembly> allAssemblies = new List<Assembly>();

            var modules = this.getModules();

            foreach (ModuleContainer module in modules)
            {
                string assembyName = module.GetProperty("AssemblyName").ToString();
                Assembly assembly = Assembly.Load(new AssemblyName(assembyName));
                allAssemblies.Add(assembly);
            }

            return allAssemblies;
        }

        public ModuleContainer[] getModules()
        {
            XmlFileParser parser = new XmlFileParser(this.FullDirectoryPath);
            var modules = parser.GetModulesFromXml();

            return modules;
        }
    }
}