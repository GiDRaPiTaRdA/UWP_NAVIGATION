using System;
using Windows.UI.Xaml.Controls;

namespace Navigator.Navigation.History
{
    public class HistoryRecord
    {
        public HistoryRecord(Frame parrentFrame, Type pageType)
        {
            this.ParrentFrame = parrentFrame;
            this.PageFullName = pageType.FullName;
        }
        public HistoryRecord(Frame parrentFrame,string pageFullName)
        {
            this.ParrentFrame = parrentFrame;
            this.PageFullName = pageFullName;
        }

        public string PageFullName { get; }
        public Frame ParrentFrame { get; set; }


        public override string ToString()
        {
            return this.PageFullName;
        }

    }
}