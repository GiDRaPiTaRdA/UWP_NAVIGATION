using System;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;

namespace Navigator.PagesManagment
{
    public class PagesManager
    {
        private static Page[] pagesTemporaryStorage;

        public static Page[] Pages
        {
            get
            {
                if (pagesTemporaryStorage == null)
                    throw new ElementNotEnabledException();
                return pagesTemporaryStorage;
            }
        }

        public void InitializePages(Page[] pages)
        {
            pagesTemporaryStorage = pages;
        }

    

        public Page GetPageByString(string pageName)
        {
            Page pageResult = null;

            foreach (var page in Pages)
            {
                if (page.ToString()==pageName)
                {
                    var a = page.ToString();
                    pageResult = page;
                    break;
                }
            }
            return pageResult;
        }

        public Type GetTypeOfPageByString(string pageName)
        {
            Type type = this.GetPageByString(pageName).GetType();
            return type;
        }
    }
}