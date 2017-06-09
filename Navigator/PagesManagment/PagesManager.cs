using System;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;

namespace Navigator.PagesManagment
{
    public class PagesManager
    {
        public PagesManagerEventHandler EventHandler => new PagesManagerEventHandler();

        private static Type[] pagesTemporaryStorage;

        public static Type[] Pages
        {
            get
            {
                if (pagesTemporaryStorage == null)
                    throw new ElementNotEnabledException();
                return pagesTemporaryStorage;
            }
        }

        public void InitializePages(Type[] pageTypes)
        {
            pagesTemporaryStorage = pageTypes;
            this.EventHandler.OnPagesInitialized(this);
        }

        public Type GetTypeOfPageByString(string pageName)
        {
            Type pageResult = null;

            foreach (var page in Pages)
            {
                if (page.FullName==pageName)
                {
                    pageResult = page;
                    break;
                }
            }
            return pageResult;
        }
    }
}