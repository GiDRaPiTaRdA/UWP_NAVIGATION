using System;

namespace Navigator
{
    public class Navigation
    {
        private static readonly Lazy<NavigationManager> LazyNavigationManager = new Lazy<NavigationManager>();

        public static NavigationManager NavigationManager => LazyNavigationManager.Value;
    }
}