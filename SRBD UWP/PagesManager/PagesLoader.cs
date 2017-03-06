using System.IO;
using System.Linq;
using Windows.UI.Xaml.Controls;
using SRBD_UWP.ImportManager;
using SRBD_UWP.PackagesConfig;

namespace SRBD_UWP.PagesManager
{
    public static class PagesLoader
    {
        public static Page[] Pages { get; set; }

        public static void LoadImports()
        {
            Pages = ImportsLoader.ImportObjects<Page>(AssemblyLoader.getAssemblies(Directory.GetCurrentDirectory())).ToArray(); 
        }
        
    }
}