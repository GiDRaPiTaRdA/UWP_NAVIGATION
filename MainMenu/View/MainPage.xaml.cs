﻿using System.Composition;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MainMenu.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MainMenu.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [Export(typeof(Page))]
    public sealed partial class MainPage : Page
    {
        private bool dataContextInitialzed;

        public MainPage()
        {
            this.InitializeComponent();

            if(this.DataContext==null)
                this.DataContext = new MainMenuViewModel();

        }
    }
}

