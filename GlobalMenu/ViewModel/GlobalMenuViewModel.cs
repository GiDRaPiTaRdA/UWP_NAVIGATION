using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using GlobalMenu.View;
using Navigator.Annotations;
using Navigator.FrameControl;
using Navigator.Navigation;
using Navigator.Navigation.History;
using Prism.Commands;


namespace GlobalMenu.ViewModel
{
   
    public sealed class GlobalMenuViewModel:INotifyPropertyChanged
    {
        private readonly string frameName = Static.NavigationFrames.MainMenuFrame;

        

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
                NavigationManager.Instance.NavigateFrame(this.frameName, "MainMenu.View.MainPage");

        }

        private void InitializeDelegateCommands()
        {
            this.NavigateCommand = new DelegateCommand<HistoryRecord>(
                (h) =>
                {
                    
                    NavigationManager.Instance.NavigateFrameSilentByHistoryRecord(h);
                });

            this.GoBack = new DelegateCommand(() => NavigationManager.Instance.NavigateBack(),
                                              () => NavigationManager.Instance.CanNavigateBack());

            this.GoForward = new DelegateCommand(() => NavigationManager.Instance.NavigateForward(),
                                                 () => NavigationManager.Instance.CanNavigateForward());

            this.GoHome = new DelegateCommand(
                () =>
                {
                    NavigationManager.Instance.NavigateFrameSilent(Static.NavigationFrames.WrapperFrame, "MainMenu.View.LoginPage");
                   // NavigationManager.Instance.History.Clear();
                });

        }

        #region INotifyPropertyChangedImplemented
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}