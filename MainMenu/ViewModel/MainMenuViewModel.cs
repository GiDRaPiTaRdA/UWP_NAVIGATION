using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using MainMenu.Models;
using MainMenu.View;
using Navigator.Navigation;
using Prism.Commands;
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

                                                NavigationWrapper.NavigationManager.History.ClearAfter(typeof(MainPage));

                                                NavigationWrapper.NavigationManager.NavigateFrame(
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