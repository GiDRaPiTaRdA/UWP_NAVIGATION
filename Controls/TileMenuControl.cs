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
        private static readonly DelegateCommand<MenuButton> defaultItemClickCommand = new DelegateCommand<MenuButton>(but =>
            {
                if (but.NavigationPageName != null)
                {
                    NavigationManager.Instance.NavigateFrame(but.ParrentFrame, but.NavigationPageName);
                }
            });

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
                typeof(DelegateCommand<MenuButton>),
                typeof(TileMenuControl),
                new PropertyMetadata(defaultItemClickCommand));

        #endregion

        #region DelegateCommands
        public DelegateCommand<MenuButton> ItemClickCommand
        {
            get { return (DelegateCommand<MenuButton>)this.GetValue(ItemClickDependencyProperty); }
            set { this.SetValue(ItemClickDependencyProperty, this.ItemClickCommand); }
        }
        #endregion

        #region Initialize
        public TileMenuControl()
        {
            this.DefaultStyleKey = typeof(TileMenuControl);
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
            {
                throw new ArgumentNullException(
                    "ItemClickCommand. Error while binding to ItemClickCommand - possibly bad binding key or property initialization is absent");
            }

            
            this.ItemClickCommand.Execute(e.ClickedItem as MenuButton);
        }

        private void InitializeButtonsSourse()
        {
            if (this.ItemsSource == null)
            {
                this.ItemsSource = new ObservableCollection<MenuButton>(this.Items.Cast<MenuButton>());
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
