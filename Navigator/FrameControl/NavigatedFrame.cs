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
using Navigator.StaticRegisters.FramesRegister;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace Navigator.FrameControl
{
    public sealed class NavigatedFrame : Frame
    {
        public NavigatedFrame()
        {
            this.DefaultStyleKey = typeof(NavigatedFrame);
          //  this.Loaded += (sender, args) => FramesRegister.AddFrameRegister(new NavigationFrame(this, this.SourcePageType, this.HistoryKey));
        }

        public static readonly DependencyProperty SourcePageTypeDP =
            DependencyProperty.Register("SourcePageType", typeof(Type), typeof(NavigationFrame), new PropertyMetadata(null));

        public static readonly DependencyProperty HistoryKeyDP =
            DependencyProperty.Register("HistoryKey", typeof(string), typeof(NavigationFrame), new PropertyMetadata(null));

        public Type SourcePageType
        {
            get { return (Type)this.GetValue(SourcePageTypeDP); }
            set { this.SetValue(SourcePageTypeDP, value); }
        }
        public string HistoryKey
        {
            get { return (string)this.GetValue(HistoryKeyDP); }
            set { this.SetValue(HistoryKeyDP, value); }
        }
    }
}
