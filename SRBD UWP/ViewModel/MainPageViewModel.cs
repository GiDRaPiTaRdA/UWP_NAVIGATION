using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Prism.Commands;
using Navigator;
using Navigator.Navigation;
using SRBD_UWP.PackagesConfig;
using SRBD_UWP.ImportManager;

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

        private void Initialize()
        {
            this.NavigateToGlobalMenuCommand =  new DelegateCommand(() =>
            NavigationWrapper.NavigationManager.NavigateFrameSilent(
                Window.Current.Content as Frame, typeof(GlobalMenu.View.MainPage)));

            this.ExitAppCommand = new DelegateCommand(Exit);    
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
