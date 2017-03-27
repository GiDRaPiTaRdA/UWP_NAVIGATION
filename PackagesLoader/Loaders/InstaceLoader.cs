using System.Linq;
using Windows.UI.Xaml.Controls;
using LoadingsManager;

namespace LoadingsManager.Loaders
{
    public class InstaceLoader : Loader
    {
        public InstaceLoader(string configFilePath)
            : base(configFilePath){}

        public T[] LoadImportingPages<T>()
        {
            AssemblyLoader assemblyLoader = new AssemblyLoader(this.ConfigFilePath);
            var instances = ImportsLoader.ImportObjects<T>(assemblyLoader.getAssemblies()).ToArray();
            return instances;
        }
    }
}