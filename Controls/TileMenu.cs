using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Prism.Commands;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace Controls
{
    public sealed class TileMenu : GridView
    {
        public static readonly DependencyProperty ItemClickDependencyProperty = DependencyProperty.Register("ItemClick", typeof(DelegateCommand<object>), typeof(TileMenu), new PropertyMetadata(null));


        public DelegateCommand<object> ItemClickCommand
        {
            get { return (DelegateCommand<object>)this.GetValue(ItemClickDependencyProperty); }
            set { this.SetValue(ItemClickDependencyProperty, this.ItemClickCommand); }
        }

        public TileMenu()
        {
            this.DefaultStyleKey = typeof(TileMenu);
            this.ItemClick += this.MyGridView_ItemClick;
        }

        private void MyGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.ItemClickCommand == null)
                throw new ArgumentNullException("ItemClickCommand. Error while binding to ItemClickCommand - possibly bad binding key or property initialization is absent");

            this.ItemClickCommand.Execute(e.ClickedItem);
        }
    }
}
