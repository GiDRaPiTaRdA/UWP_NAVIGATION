using System;

namespace Navigator
{
    public class HistoryRecord
    {
        public HistoryRecord() : this(null) { }
        public HistoryRecord(Type pageType)
        {
            this.PageType = pageType;
        }

        public Type PageType { get; private set; }

        public string PageTitle => this.PageType.Name;

        public override bool Equals(object obj)
        {
            return this.PageType.FullName == (obj as HistoryRecord)?.PageType.FullName;
        }
    }
}