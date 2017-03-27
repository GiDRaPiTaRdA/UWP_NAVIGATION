using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantArgumentDefaultValue
// ReSharper disable UnusedMethodReturnValue.Global

namespace Navigator.Navigation.History
{
    /// <summary>
    /// The class which describes History Object for navigation
    /// </summary>
    public class NavigationHistory
    {

        #region Properties
        internal LinkedListNode<HistoryRecord> CurrentNode { get; set; }
        internal LinkedList<HistoryRecord> FrameNavigationHistory { get; set; }
        #endregion

        #region Initialization

        /// <summary>
        /// Constructor without initializing default page
        /// </summary>
        public NavigationHistory()
        {
            this.FrameNavigationHistory = new LinkedList<HistoryRecord>();
        }

        #endregion

        #region Operations on history

        #region Operations TYPE
        /// <summary>
        /// Adds page type to the history
        /// </summary>
        /// <param name="frameType">Adding frame type</param>
        /// <returns>success of the operation</returns>
        internal bool Add(Type frameType)
        {
            return this.Add(frameType.FullName);
        }

        /// <summary>
        /// Removes specified page type from the page types list (history)
        /// </summary>
        /// <param name="frameType">Removing type</param>
        /// <returns>success of the operation</returns>
        internal bool Remove(Type frameType)
        {    
            return this.Remove(frameType.FullName);
        }

        public void ClearAfter(Type frameType)
        {
            this.ClearAfter(frameType.FullName);
        }
        #endregion

        #region Operations STRING
        /// <summary>
        /// Adds page type to the history
        /// </summary>
        /// <param name="typeName">Adding frame type name</param>
        /// <returns>success of the operation</returns>
        internal bool Add(string typeName)
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(typeName))
            {
                this.FrameNavigationHistory.AddLast(new HistoryRecord(typeName));
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Removes specified page type from the page types list (history)
        /// </summary>
        /// <param name="typeName">Removing type name</param>
        /// <returns>success of the operation</returns>
        internal bool Remove(string typeName)
        {
            bool result = this.FrameNavigationHistory.Remove(
                this.FrameNavigationHistory.FirstOrDefault(t => t.PageFullName == typeName));

            return result;
        }

        public void ClearAfter(string frameName)
        {
            LinkedList<HistoryRecord> resultingHistoryRecords = new LinkedList<HistoryRecord>();

            foreach (var historyRecord in this.FrameNavigationHistory)
            {
                resultingHistoryRecords.AddLast(historyRecord);

                if (historyRecord.PageFullName == frameName)
                {
                    this.FrameNavigationHistory = resultingHistoryRecords;
                    return;
                }

            }
        }

        public void ClearAfter(HistoryRecord record)
        {
            LinkedList<HistoryRecord> resultingHistoryRecords = new LinkedList<HistoryRecord>();

            foreach (var historyRecord in this.FrameNavigationHistory)
            {
                resultingHistoryRecords.AddLast(historyRecord);

                if (historyRecord.Equals(record))
                {
                    this.FrameNavigationHistory = resultingHistoryRecords;
                    return;
                }

            }
        }
        #endregion

        /// <summary>
        /// Clears list from values and resets index to -1
        /// </summary>
        public void Clear() => this.FrameNavigationHistory.Clear();

        public bool Any(Func<HistoryRecord,bool> func = null)
        {
            var result = false;
            if(func==null)
                result = this.FrameNavigationHistory.Any();
            else
                result = this.FrameNavigationHistory.Any(func);

            return result;
        }

        #endregion

        #region GetInfoAboutHistory
    
        public HistoryRecord[] GetHistoryAsArray => this.FrameNavigationHistory.ToArray();

        /// <summary>
        /// Counts items in the page types list (history)
        /// </summary>
        /// <returns>ammount of items in the list (history)</returns>
        internal int Count() => this.FrameNavigationHistory.Count;

        /// <summary>
        /// Gets the next value of the history
        /// </summary>
        /// <returns>the type of page the current item of history</returns>
        internal HistoryRecord GetCurrent() => this.CurrentNode?.Value;
        /// <summary>
        /// Gets the next value of the history
        /// </summary>
        /// <returns>the type of page the next item of history</returns>
        internal HistoryRecord GetNext() => this.CurrentNode.Next?.Value;

        /// <summary>
        /// Gets the previous value of the history
        /// </summary>
        /// <returns>the type of page the previous item of history</returns>
        internal HistoryRecord GetPrevious() => this.CurrentNode.Previous?.Value;

        #endregion
    }
}