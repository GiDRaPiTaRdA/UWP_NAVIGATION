﻿using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using MainMenu.Models;
using MainMenu.View;
using Navigator.FrameControl;
using Navigator.Navigation;
using Prism.Commands;
using PropertyChanged;

namespace MainMenu.ViewModel
{
    [ImplementPropertyChanged]
    public class AditionalMenuViewModel
    {
        public ObservableCollection<MainMenuPageButton> MainMenuPageButtonsList { get; set; }

    public DelegateCommand<object> NavigateCurrentCommand { get; set; }

        public NavigationFrame NaviFrame { get; set; }

        public AditionalMenuViewModel()
            : this(frame: null){ }
        public AditionalMenuViewModel(Frame frame = null)
        {
            //if(frame!=null)
            //    this.NaviFrame = new NavigationFrame() {FrameName = ""};

            this.Initialize();
        }

        private void Initialize()
        {
            string imagePath = "ms-appx:///MainMenu/TileIcons/SplashScreen.scale-200.png";

            this.NavigateCurrentCommand = new DelegateCommand<object>(obj =>
            {

                MainMenuPageButton button = (MainMenuPageButton)obj;

                if (button.NavigationPageName != null)
                    NavigationManager.Instance.NavigateFrame("MainMenuFrame", button.NavigationPageName);
            }
            );

            if (this.MainMenuPageButtonsList==null)
            this.MainMenuPageButtonsList = new ObservableCollection<MainMenuPageButton>()
            {
                   new MainMenuPageButton(typeof(BlankPage1),"TITLE0",imagePath),
                   new MainMenuPageButton(typeof(BlankPage2),"TITLE1",imagePath),
                   new MainMenuPageButton(typeof(BlankPage3),"TITLE2",imagePath),
                   new MainMenuPageButton(typeof(BlankPage4),"TITLE3",imagePath),
                   
            };
        }
    }
}