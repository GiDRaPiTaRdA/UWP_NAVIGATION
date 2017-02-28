using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MainMenu.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MainMenu.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {  
        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += (sender, args) => this.DataContext = new MainMenuViewModel(this.Frame);

            
        }
    }
}

