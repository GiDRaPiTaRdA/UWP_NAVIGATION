using System;
using Windows.UI.Xaml.Controls;
using MainMenu.View;
using Navigator;
using Navigator.Navigation;
using Prism.Commands;

namespace MainMenu.ViewModel
{
    public class BlankPage1ViewModel
    {
        public DelegateCommand NavigateCommand { get; set; }

        public BlankPage1ViewModel()
        { 
            this.Initalize();
        }

        private void Initalize()
        {
            this.NavigateCommand =
                new DelegateCommand(() =>
                NavigationManager.Instance.NavigateFrame(
                    "MainMenuFrame", typeof(BlankPage2).FullName));      
                          
        }
    }
}