using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.Devices.Midi;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MainMenu.Models;
using MainMenu.View;
using Navigator;
using Navigator.StaticRegisters.FramesRegister;
using Prism.Commands;
using Prism.Events;
using PropertyChanged;

namespace MainMenu.ViewModel
{
    [ImplementPropertyChanged]
    public class MainMenuViewModel
    {
        public ObservableCollection<MainMenuPageButton> MainMenuPageButtonsList { get; set; }

        public DelegateCommand<object> NavigateCurrentCommand { get; set; }

        public NavigationFrame NaviFrame { get; set; }

        public MainMenuViewModel()
            : this(frame: null){ }
        public MainMenuViewModel(Frame frame = null)
        {
            
            if(frame!=null)
                this.NaviFrame = new NavigationFrame(frame);

            this.Initialize();

            

        }

        private void Initialize()
        {
            this.NavigateCurrentCommand =
                new DelegateCommand<object>(obj =>
                                            {

                                                Navigation.NavigationManager.History.ClearAfter(typeof(MainPage));

                                                Navigation.NavigationManager.NavigateFrame(
                                                    this.NaviFrame?.CurrentFrame,
                                                    ((MainMenuPageButton)obj).NavigationPageType);

                                                
                                            }

    );

            this.MainMenuPageButtonsList = new ObservableCollection<MainMenuPageButton>()
            {
                   new MainMenuPageButton(typeof(BlankPage1),"TITLE0","../TileIcons/LockScreenLogo.png"),
                   new MainMenuPageButton(typeof(BlankPage2),"TITLE1","../TileIcons/LockScreenLogo.png"),
                   new MainMenuPageButton(typeof(BlankPage3),"TITLE2","../TileIcons/LockScreenLogo.png"),
                   new MainMenuPageButton(typeof(BlankPage4),"TITLE3","../TileIcons/LockScreenLogo.png"),
                   new MainMenuPageButton(typeof(BlankPage1)),
                   new MainMenuPageButton(typeof(BlankPage1)),
                   new MainMenuPageButton(typeof(BlankPage1)),
            };
        }
    }
}