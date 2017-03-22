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
            this.Loading += (sender, e)=> NavigationManager.Instance.FrameManager.AddFrame(this);
        }

        /* //LABUDA
        public static readonly DependencyProperty FrameNameDependencyProperty =
            DependencyProperty.Register("FrameName",
                 typeof(string),
                 typeof(NavigationFrame),
                 new PropertyMetadata(null));

        public string FrameName
        {
            get
            {
                var a = (string)this.GetValue(FrameNameDependencyProperty);
                return a;
            }
            set { this.SetValue(FrameNameDependencyProperty, value); }
        }
        */

        public string FrameName { get; set; }
    }
}
