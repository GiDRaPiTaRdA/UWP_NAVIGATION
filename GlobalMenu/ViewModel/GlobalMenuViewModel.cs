using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using GlobalMenu.View;
using MainMenu.Models;
using Navigator;
using Navigator.Annotations;
using Navigator.FrameControl;
using Navigator.Navigation;
using Navigator.Navigation.History;
using Prism.Commands;
using PropertyChanged;
using Static;

namespace GlobalMenu.ViewModel
{
   
    public class GlobalMenuViewModel:INotifyPropertyChanged
    {
        public string Frame { get; set; } = "MainMenuFrame";

        public int a => 0;

        public HistoryRecord[] History => NavigationManager.Instance.History.GetHistoryAsArray;

        #region DelegateCommands
        public DelegateCommand<HistoryRecord> NavigateCommand { get; set; }
        public DelegateCommand GoHome { get; set; }
        public DelegateCommand GoBack { get; set; }
        public DelegateCommand GoForward { get; set; }

        #endregion


        public GlobalMenuViewModel()
        {
           
            this.InitializeDelegateCommands();

            NavigationManager.Instance.NavigationEventHandler.EventHandler += (sender, args) =>
                                                                                {
                                                                                        this.GoBack.RaiseCanExecuteChanged();
                                                                                        this.GoForward.RaiseCanExecuteChanged();
                                                                                        this.OnPropertyChanged(nameof(this.History));              
                                                                                };

            //NavigationManager.Instance.NavigateFrame(this.Frame, "MainMenu.View.MainPage");

        }

        private void InitializeDelegateCommands()
        {
            this.NavigateCommand = new DelegateCommand<HistoryRecord>(
                (h) => NavigationManager.Instance.NavigateFrame(this.Frame, h.FullPageName));

            this.GoBack = new DelegateCommand(() => NavigationManager.Instance.NavigateBack(this.Frame),
                                              () => NavigationManager.Instance.CanNavigateBack());

            this.GoForward = new DelegateCommand(() => NavigationManager.Instance.NavigateForward(this.Frame),
                                                 () => NavigationManager.Instance.CanNavigateForward());

        }

        #region INotifyPropertyChangedImplemented
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}