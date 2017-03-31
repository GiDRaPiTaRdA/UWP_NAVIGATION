using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Navigator.Navigation;
using Prism.Commands;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace Controls
{
    public sealed class TileMenuControl : GridView
    {
        private readonly DelegateCommand<object> defaultItemClickCommand;

        #region DependencyProperties
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(
                "Items",
                typeof(IList),
                typeof(TileMenuControl),
                new PropertyMetadata(new List<MenuButton>()));

        public static readonly DependencyProperty ItemClickDependencyProperty =
            DependencyProperty.Register(
                "ItemClickCommand",
                typeof(DelegateCommand<object>),
                typeof(TileMenuControl),
                null);

        #endregion

        #region DelegateCommands
        public DelegateCommand<object> ItemClickCommand
        {
            get
            {
                // If ItemClickDependencyProperty value is not initialized so use default command
                return (DelegateCommand<object>)this.GetValue(ItemClickDependencyProperty)?? this.defaultItemClickCommand;
            }
            set { this.SetValue(ItemClickDependencyProperty, this.ItemClickCommand); }
        }
        #endregion

        #region Initialize
        public TileMenuControl()
        {
            this.DefaultStyleKey = typeof(TileMenuControl);

            this.defaultItemClickCommand = new DelegateCommand<object>(obj =>
            {

                MenuButton button = (MenuButton)obj;

                if (button.NavigationPageName != null)
                    NavigationManager.Instance.NavigateFrame(button.ParrentFrame, button.NavigationPageName);
            }
           );
        }
       
        protected override void OnApplyTemplate()
        {
            this.InitializeButtonsSourse();

            this.SubscribeItemClick();
        }

        #endregion

        #region Methods
        private void ItemClickAction(object sender, ItemClickEventArgs e)
        {
            if (this.ItemClickCommand == null)
                throw new ArgumentNullException(
                    "ItemClickCommand. Error while binding to ItemClickCommand - possibly bad binding key or property initialization is absent");

            this.ItemClickCommand.Execute(e.ClickedItem);
        }

        private void InitializeButtonsSourse()
        {
            if (this.ItemsSource == null)
            {
                var a = this.Items;
                var colection = new ObservableCollection<MenuButton>();


                foreach (var VARIABLE in a)
                {
                    colection.Add((MenuButton)VARIABLE);
                }

                this.ItemsSource = colection;
            }

        }

        private void SubscribeItemClick()
        {
            GridView gv = this.GetTemplateChild("GridView") as GridView;
            gv.ItemClick += this.ItemClickAction;
        }

        #endregion
    }
}
