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
    public class AditionalMenuViewModel
    {
        public ObservableCollection<MainMenuPageButton> MainMenuPageButtonsList { get; set; }

    public DelegateCommand<object> NavigateCurrentCommand { get; set; }

        public NavigationFrame NaviFrame { get; set; }

        public AditionalMenuViewModel()
            : this(frame: null){ }
        public AditionalMenuViewModel(Frame frame = null)
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

                                                NavigationWrapper.NavigationManager.History.ClearAfter(typeof(AditionalMenu).FullName);

                                                NavigationWrapper.NavigationManager.NavigateFrame(
                                                    this.NaviFrame?.CurrentFrame,
                                                    ((MainMenuPageButton)obj).NavigationPageName);

                                                
                                            });

            string imagePath = "ms-appx:///MainMenu/TileIcons/SplashScreen.scale-200.png";

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