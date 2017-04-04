using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
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
    public sealed class BreadCrumbs : ItemsControl
    {
        public string Tex { get; set; } = "vgdsd";
        public DelegateCommand ClickCommand
        {
            get { return (DelegateCommand)this.GetValue(ClickCommandProperty); }
            set { this.SetValue(ClickCommandProperty, value); }
        }

        public BreadCrumbs()
        {
            this.DefaultStyleKey = typeof(BreadCrumbs);
            this.ClickCommand = new DelegateCommand(() => { int i = 0; });
        }

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register(
                "ClickCommand",
                typeof(DelegateCommand),
                typeof(BreadCrumbs), new PropertyMetadata(new DelegateCommand(() => { int i = 0; }))
                );
        

        protected override void OnApplyTemplate()
        {
            ItemsControl gv = this.GetTemplateChild("GridView") as ItemsControl;
            var template = gv.ItemTemplate;
        }

        protected override void OnItemsChanged(object e)
        {
            base.OnItemsChanged(e);
            this.UpdateLayout();

        }
    }
}
