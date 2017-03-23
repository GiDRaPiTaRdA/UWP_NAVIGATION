using System;
using Windows.UI.Xaml.Controls;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Navigator.NavigationEventsHandler
{
    public class NavigationArgs : EventArgs
    {
        public NavigationArgs() : this(null,null) { }
        public NavigationArgs(Type navigatedPageTypeType,Frame frame)
        {
            this.NavigatedPageType = navigatedPageTypeType;
            this.FrameName = frame;
        }
        public Type NavigatedPageType { get; set; }
        public Frame FrameName { get; set; }
    }
}