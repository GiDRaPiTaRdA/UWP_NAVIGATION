using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LoadingsManager;

namespace LoadingsManager.Loaders
{
    public class TypesLoader :Loader
    {
        public TypesLoader(string configFilePath)
            : base(configFilePath){}

        public Type[] LoadImportingPageTypes(Func<TypeInfo,bool> predicate)
        {
            AssemblyLoader assemblyLoader = new AssemblyLoader(this.ConfigFilePath);
            var assemblies = assemblyLoader.getAssemblies();

            List<Type> types = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<TypeInfo> infoTypes = assembly.DefinedTypes.Where(predicate);

                foreach (var infoType in infoTypes)
                {
                    types.Add(infoType.AsType());
                }
            }
            return types.ToArray();
        }
    }
}