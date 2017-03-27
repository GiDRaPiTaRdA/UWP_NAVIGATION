using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using LoadingsManager.Loaders;

namespace SRBD_UWP.PagesManager
{
    public static class Bootstrapper
    {
        public static void LoadImportingPageTypes()
        {
            TypesLoader typesLoader =  new TypesLoader("LoadingModulesConfig.xml");
            var types = typesLoader.LoadImportingPageTypes(t =>
                                                               t.CustomAttributes.Any(a =>
                                                                                          a.AttributeType
                                                                                          == typeof(Navigator.NavigationAttribute.NavigationPageAttribute)));
            Navigator.Navigation.NavigationManager.InitializePageTypes(types);
        }

    }
}