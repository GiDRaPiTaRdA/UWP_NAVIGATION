using System;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using MainMenu.View;
using Navigator.Navigation;
using Prism.Commands;

namespace MainMenu.ViewModel
{
    public class BlankPage2ViewModel
    {
        public DelegateCommand NavigateCommand { get; set; }

        public BlankPage2ViewModel(Type type,Frame frame)
        {
            this.Initialize();
        }

        public void Initialize()
        {
            this.NavigateCommand =
               new DelegateCommand(() =>
               NavigationManager.Instance.NavigateFrame(
                   "MainMenuFrame", typeof(AditionalMenu).FullName));
        }
    }
}