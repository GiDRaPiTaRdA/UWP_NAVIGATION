using System;
using Windows.UI.Xaml.Controls;

namespace Navigator.NavigationEventsHandler
{
    public sealed class NavigationEventHandler
    {
        public event EventHandler EventHandler;

        public void OnNavigated(Type pageType,Frame frame)
        {
            this.EventHandler?.Invoke(this,new NavigationArgs(pageType,frame));
        }
    }
}
