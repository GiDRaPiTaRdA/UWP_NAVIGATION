using System;
using Windows.UI.Xaml.Controls;
using MainMenu.View;
using Navigator;
using Navigator.StaticRegisters.FramesRegister;
using Prism.Commands;

namespace MainMenu.ViewModel
{
    public class BlankPage1ViewModel
    {
        public DelegateCommand NavigateCommand { get; set; }
        public DelegateCommand NavigateBackCommand { get; set; }

        public BlankPage1ViewModel(Frame frame)
        { 
            this.Initalize(frame);
        }

        private void Initalize(Frame frame)
        {
            this.NavigateCommand =
                new DelegateCommand(() =>
                Navigation.NavigationManager.NavigateFrame(
                    frame, typeof(BlankPage2)));      
                          
        }
    }
}