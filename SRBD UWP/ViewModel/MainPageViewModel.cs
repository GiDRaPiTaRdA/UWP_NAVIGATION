using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Prism.Commands;
using Navigator;
using Navigator.StaticRegisters.FramesRegister;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ArrangeTypeModifiers

namespace SRBD_UWP.ViewModel
{
    class MainPageViewModel
    { 
        public DelegateCommand NavigateToGlobalMenuCommand { get; set; }
        public DelegateCommand ExitAppCommand { get; set; }
        public MainPageViewModel()
        {
            this.Initialize();
        }

        private async void Initialize()
        {
            this.NavigateToGlobalMenuCommand =  new DelegateCommand(() =>
            Navigation.NavigationManager.NavigateFrameSilent(
                Window.Current.Content as Frame, typeof(GlobalMenu.View.MainPage)));

            this.ExitAppCommand = new DelegateCommand(Exit);

            //await Notification("BLANCKPAGE1's property \'NavigationCacheMode\" is set to Enabled in XAML", "Notification");
        }

        private static async void Exit()
        {
            if (await YesNoAsyncDialog("Do you want to leave the application?", "Leaving the app"))
                Application.Current.Exit();
        }
        private static async Task<bool> YesNoAsyncDialog(string message, string title)
        {
            var dialog = new MessageDialog(message, title);
            dialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new UICommand("No") { Id = 1 });
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;
            var dialogResult = await dialog.ShowAsync();
            if ((int)dialogResult.Id == 0)
                return true;
            else
                return false;
        }
        private static async Task Notification(string message, string title = null)
        {
            if (title != null)
            {
                var dialog = new MessageDialog(message, title);
                dialog.Commands.Add(new UICommand("Ok"));
                await dialog.ShowAsync();
            }
        }
    }
}
