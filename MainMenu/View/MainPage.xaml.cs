using System.Composition;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MainMenu.ViewModel;
using Navigator.NavigationAttribute;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MainMenu.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [NavigationPage]
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
    }
}

