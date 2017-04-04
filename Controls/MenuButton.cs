using System;
using System.Globalization;
using System.IO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Controls
{
    public class MenuButton
    {
        //todo Create here navigation Command!!! :)

        public string NavigationPageName { get; set; }
        public string ParrentFrame { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }

        public MenuButton() {}
        public MenuButton(string page,string parrent, string title = null, string image = null)
        {
            this.NavigationPageName = page;
            this.ParrentFrame = parrent;
            this.Title = title;
            this.Image = image;
        }

        public MenuButton(Type page, string parrent, string title = null, string image = null)
            : this(page.FullName, parrent, title, image) { }
    }
}