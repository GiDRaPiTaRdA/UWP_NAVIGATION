using System;

namespace Navigator.Navigation.History
{
    public class HistoryRecord
    {
        public HistoryRecord(Type pageType)
        {
            this.FullPageName = pageType.FullName;
        }
        public HistoryRecord(string fullPageName)
        {
            this.FullPageName = fullPageName;
        }

        public string FullPageName { get; private set; }

        public override bool Equals(object obj)
        {
            return this.FullPageName == (obj as HistoryRecord)?.FullPageName;
        }

        public override string ToString()
        {
            return this.FullPageName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}