using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Navigator.FrameControl;

namespace Navigator.FramesManagment
{
    public class FrameManager
    {
        public Dictionary<string, NavigationFrame> Frames { get; private set; }

        public FrameManager()
        {
            this.Frames =  new Dictionary<string, NavigationFrame>();
        }

        public void InitializeFrames(NavigationFrame[] frames)
        {
            foreach (var navigationFrame in frames)
            {
                this.Frames.Add(navigationFrame.FrameName,navigationFrame);
            }
        }

        public void AddFrame(NavigationFrame navigationFrame)
        {
            if(!this.Frames.ContainsKey(navigationFrame.FrameName))
            this.Frames.Add(navigationFrame.FrameName,navigationFrame);
        }

        public NavigationFrame GetFrameByString(string frameName)
        {
            NavigationFrame frameResult=  this.Frames[frameName];

            return frameResult;
        }
    }
}