using System;

namespace Navigator
{
    public sealed class NavigationEventHandler
    {
        public event EventHandler EventHandler;

        public void OnNavigated(Type pageType)
        {
            this.EventHandler?.Invoke(this,new NavigationArgs(pageType));
        }
    }
}
