using System;
using Windows.UI.Xaml.Controls;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Navigator.NavigationEventsHandler
{
    public class NavigationArgs : EventArgs
    {
        public NavigationArgs() : this(null,null) { }
        public NavigationArgs(Page navigatedPage,Frame frame)
        {
            this.NavigatedPage = navigatedPage;
            this.FrameName = frame;
        }
        public Page NavigatedPage { get; set; }
        public Frame FrameName { get; set; }
    }
}