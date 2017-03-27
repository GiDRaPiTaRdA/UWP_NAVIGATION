using System;

namespace Navigator.Navigation.History
{
    public class HistoryRecord
    {
        public HistoryRecord(Type pageType)
        {
            this.PageFullName = pageType.FullName;
        }
        public HistoryRecord(string pageFullName)
        {
            this.PageFullName = pageFullName;
        }

        public string PageFullName { get; private set; }



        public override string ToString()
        {
            return this.PageFullName;
        }

    }
}