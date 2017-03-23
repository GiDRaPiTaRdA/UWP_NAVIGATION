using System.Collections.ObjectModel;
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
    public class MainMenuViewModel
    {
        public ObservableCollection<MainMenuPageButton> MainMenuPageButtonsList { get; set; }

    public DelegateCommand<object> NavigateCurrentCommand { get; set; }

        public NavigationFrame NaviFrame { get; set; }

        public MainMenuViewModel(Frame frame = null)
        {
            //if(frame!=null)
            //    this.NaviFrame = new NavigationFrame();

            this.Initialize();
        }

        private void Initialize()
        {
            this.NavigateCurrentCommand =  new DelegateCommand<object>(obj=>
                                                                       {
                                                                           MainMenuPageButton button = (MainMenuPageButton)obj;
                NavigationManager.Instance.NavigateFrame("MainMenuFrame", button.NavigationPageName);
            }
            );
         
            string imagePath = "ms-appx:///MainMenu/TileIcons/SplashScreen.scale-200.png";

            if (this.MainMenuPageButtonsList==null)
            this.MainMenuPageButtonsList = new ObservableCollection<MainMenuPageButton>()
            {
                   new MainMenuPageButton(typeof(BlankPage1),"TITLE0",imagePath),
                   new MainMenuPageButton(typeof(BlankPage2),"TITLE1",imagePath),
                   new MainMenuPageButton(typeof(ImageTest),"Image Test",imagePath),
                   new MainMenuPageButton(),
                   new MainMenuPageButton(),
                   new MainMenuPageButton(),
                   new MainMenuPageButton(typeof(AditionalMenu),"MENU",imagePath),
            };
        }
    }
}