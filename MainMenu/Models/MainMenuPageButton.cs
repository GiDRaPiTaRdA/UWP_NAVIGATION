using System;
using System.Globalization;
using System.IO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace MainMenu.Models
{
    public class MainMenuPageButton
    {
        public Type NavigationPageType { get; set; }
        public string Title { get; set; }
        public string History { get; set; }
        public string Image { get; set; }

        public MainMenuPageButton() {}
        public MainMenuPageButton(Type page, string title = null, string image = null)
        {
            this.NavigationPageType = page;
            this.Title = title;
            this.Image = image;
        }
    }
}