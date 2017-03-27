using System;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using MainMenu.View;
using Prism.Commands;

namespace MainMenu.ViewModel
{
    public class BlankPage4ViewModel
    {
        public DelegateCommand NavigateCommand { get; set; }

        public BlankPage4ViewModel()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.NavigateCommand =  new DelegateCommand(()=>
            Navigator.Navigation.NavigationManager.Instance.NavigateFrameSilent(
                Static.NavigationFrames.WrapperFrame,
                "GlobalMenu.View.MainPage"));
        }
    }
}