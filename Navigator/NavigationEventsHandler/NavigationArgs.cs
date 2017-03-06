using System;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Navigator.NavigationEventsHandler
{
    public class NavigationArgs : EventArgs
    {
        public NavigationArgs() : this(null) { }
        public NavigationArgs(Type navigatedPageType)
        {
            this.NavigatedPageType = navigatedPageType;
        }
        public Type NavigatedPageType { get; set; }
    }
}