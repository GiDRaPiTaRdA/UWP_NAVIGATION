using System.Composition;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GlobalMenu.ViewModel;
using Navigator.NavigationAttribute;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GlobalMenu.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [NavigationPage]
    public sealed partial class MainPage:Page
    {  
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = new GlobalMenuViewModel();
        }
    }
}

