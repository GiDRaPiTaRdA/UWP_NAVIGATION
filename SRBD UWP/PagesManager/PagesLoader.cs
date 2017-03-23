using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using ImportManager;
using SRBD_UWP.PackagesLoader;

namespace SRBD_UWP.PagesManager
{
    public static class PagesLoader
    {
        //Assembly.Load(new AssemblyName(this.GetType().AssemblyQualifiedName)).DefinedTypes.Where(t=>t.CustomAttributes.Any(ta=>ta.AttributeType == ))

        public static Page[] LoadImportingPages()
        {
            AssemblyLoader assemblyLoader =  new AssemblyLoader("LoadingModulesConfig.xml");
            var pages = ImportsLoader.ImportObjects<Page>(assemblyLoader.getAssemblies()).ToArray();
            return pages;
        }

        public static Type[] LoadImportingPageTypes()
        {
            AssemblyLoader assemblyLoader = new AssemblyLoader("LoadingModulesConfig.xml");
            var assemblies = assemblyLoader.getAssemblies();


            List<Type> types = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                // var types = assembly.DefinedTypes.Where(t => t.BaseType==typeof(Page));

                //var types = assembly.DefinedTypes.Where(t => t.CustomAttributes.Any(ta => ta.AttributeType == typeof(Export<Page>)));

                IEnumerable<TypeInfo> infoTypes = assembly.DefinedTypes.Where(t => t.BaseType == typeof(Page));

                

                foreach (var infoType in infoTypes)
                {
                    types.Add(infoType.AsType());
                }

            }

            return types.ToArray();
        }

    }
}