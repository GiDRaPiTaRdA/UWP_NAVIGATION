using System;
using Windows.UI.Xaml.Controls;
using MainMenu.View;
using Navigator;
using Navigator.Navigation;
using Prism.Commands;

namespace MainMenu.ViewModel
{
    public class BlankPage3ViewModel
    {
        public DelegateCommand NavigateCommand { get; set; }
        public DelegateCommand NavigateBackCommand { get; set; }

        public BlankPage3ViewModel()
        { 
            this.Initalize();
        }

        private void Initalize()
        {
            this.NavigateCommand =
                new DelegateCommand(() =>
                NavigationManager.Instance.NavigateFrame(
                    Static.NavigationFrames.WrapperFrame, typeof(BlankPage4).FullName));                    
        }
    }
}