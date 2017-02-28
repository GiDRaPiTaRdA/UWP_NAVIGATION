﻿using System;
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
using Navigator.StaticRegisters.FramesRegister;
using Prism.Commands;
using PropertyChanged;

namespace GlobalMenu.ViewModel
{
   
    public class GlobalMenuViewModel
    {
        public NavigationFrame NavigationFrame { get; set; }

        #region DelegateCommands
        public DelegateCommand<HistoryRecord> NavigateCommand { get; set; }
        public DelegateCommand GoHome { get; set; }
        public DelegateCommand GoBack { get; set; }
        public DelegateCommand GoForward { get; set; }

        #endregion


        public GlobalMenuViewModel(Frame frame,Frame nestedFrame)
        {
            

            this.NavigationFrame = new NavigationFrame(frame,nestedFrame);

            Navigation.NavigationManager.NavigateFrame(nestedFrame, typeof(MainMenu.View.MainPage));
            

            this.InitializeDelegateCommands();

            Navigation.NavigationManager.NavigationEventHandler.EventHandler += (sender, args) =>
                                                                                {
                                                                                        this.GoBack.RaiseCanExecuteChanged();
                                                                                        this.GoForward.RaiseCanExecuteChanged();
                                                                                };

        }

        private void InitializeDelegateCommands()
        {
            this.NavigateCommand = new DelegateCommand<HistoryRecord>((h) => Navigator.Navigation.NavigationManager.NavigateFrame(this.NavigationFrame.CurrentFrame, h.PageType));

            this.GoBack = new DelegateCommand(() => Navigation.NavigationManager.NavigateBack(this.NavigationFrame.CurrentFrame,this.NavigationFrame.NestedFrame),
                                              () => Navigation.NavigationManager.CanNavigateBack());

            this.GoForward = new DelegateCommand(() => Navigation.NavigationManager.NavigateForward(this.NavigationFrame.NestedFrame),
                                                 () => Navigation.NavigationManager.CanNavigateForward());

            this.GoHome =  new DelegateCommand(()=>this.NavigationFrame.CurrentFrame.GoBack());
        }
    }
}