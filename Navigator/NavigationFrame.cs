using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using PropertyChanged;

namespace Navigator.StaticRegisters.FramesRegister
{
    [ImplementPropertyChanged]
    public class NavigationFrame
    {

        public NavigationFrame(): this(null) { }

        public NavigationFrame(Frame currentFrame, Frame nestedFrame = null)
        {
            this.CurrentFrame = currentFrame;
            this.NestedFrame = nestedFrame;
        }

        public Frame CurrentFrame { get; set; }
        public Frame NestedFrame { get; set; }
    }
}
