using System;
using Windows.Foundation.Diagnostics;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Navigator.Navigation;
using PropertyChanged;

namespace Navigator.FrameControl
{
    [ImplementPropertyChanged]
    public class NavigationFrame :Frame
    {

        public NavigationFrame()
        {
            string s = this.FrameName;
        }

        //LABUDA
        public static readonly DependencyProperty FrameNameDependencyProperty =
            DependencyProperty.Register("FrameName",
                 typeof(string),
                 typeof(NavigationFrame),
                 new PropertyMetadata(null, OnFrameNameDpChanged));

        private static void OnFrameNameDpChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue && e.OldValue != null)
            { throw new Exception("Modifying property FrameName is forbidden");}

            var nf = d as NavigationFrame;
            NavigationManager.Instance.FrameManager.AddFrame(nf);
        }

        public string FrameName
        {
            get{return (string)this.GetValue(FrameNameDependencyProperty);}
            set { this.SetValue(FrameNameDependencyProperty, value); }
        }
    }
}
