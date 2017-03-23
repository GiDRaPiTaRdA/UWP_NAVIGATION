using System;

namespace Navigator.PagesManagment
{
    public class PagesManagerEventHandler
    {
        public event EventHandler PagesHandler;

        public void OnPagesInitialized(PagesManager pagesManager)
        {
            this.PagesHandler?.Invoke(pagesManager, null);
        }
    }
}