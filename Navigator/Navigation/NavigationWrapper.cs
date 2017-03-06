using System;
using Navigator.Navigation;

namespace Navigator.Navigation
{
    public static class NavigationWrapper
    {
        private static readonly Lazy<NavigationManager> LazyNavigationManager = new Lazy<NavigationManager>();

        public static NavigationManager NavigationManager => LazyNavigationManager.Value;
    }
}