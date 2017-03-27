using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Navigator.FramesManagment;
using Navigator.Navigation.History;
using Navigator.NavigationEventsHandler;
using Navigator.PagesManagment;
using PropertyChanged;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global BINDABLE PROPS

namespace Navigator.Navigation
{
    [ImplementPropertyChanged]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class NavigationManager
    {

        private static readonly Lazy<NavigationManager> LazyNavigationManager = new Lazy<NavigationManager>(()=>new NavigationManager());

        public static NavigationManager Instance => LazyNavigationManager.Value;

        #region Properties
        /// <summary>
        /// navigation event handler raises events for outer classes ( for example the very basic event onNavigated )
        /// </summary>
        public NavigationEventHandler NavigationEventHandler { get; }
        /// <summary>
        /// History of navigation
        /// </summary>
        public NavigationHistory History { get; }

        /// <summary>
        /// this obj contains list of pages in it and operation with them
        /// </summary>
        public static PagesManager PagesManager { get; } = new PagesManager();
        /// <summary>
        /// this obj contains all loaded NavigatonFrames
        /// </summary>
        public FrameManager FrameManager { get; }
        #endregion

        internal NavigationManager()
        {
            this.History = new NavigationHistory();
            
            this.FrameManager = new FrameManager();
            this.NavigationEventHandler = new NavigationEventHandler();
        }

        #region ________________________PageNavigation_________________________

        #region Navigation

        #region Navigate by Srting
        /// <summary>
        /// Navigate frame to type by string
        /// </summary>
        public void NavigateFrame(string frame, string pageName)
        {
            Type t = PagesManager.GetTypeOfPageByString(pageName);

            this.NavigateFrame(frame, t);
        }

        /// <summary>
        /// Navigates frame to type without adding to history
        /// </summary>
        public void NavigateFrameSilent(string frameName, string pageName)
        {
            this.NavigateFrameSilent(frameName, PagesManager.GetTypeOfPageByString(pageName)); // TODO NO DEFAULT NAVIGATION!!!
        }
        #endregion

        #region Navigate by Type
        /// <summary>
        /// Navigate frame to type
        /// </summary>
        public void NavigateFrame(string frameName, Type targetType)
        {
            Frame frame = this.FrameManager.GetFrameByString(frameName);

            frame.Navigate(targetType); // TODO NO DEFAULT NAVIGATION!!!

            this.History.ClearAfter(this.History.GetCurrent());

            this.History.Add(targetType);

            this.SetHistoryCurrentNode(this.History.FrameNavigationHistory.Last);

            this.RaizeOnNavigated(PagesManager.GetTypeOfPageByString(targetType.FullName), frame);
        }

        /// <summary>
        /// Navigates frame to type without adding to history
        /// </summary>
        public void NavigateFrameSilent(string frameName, Type targetType)
        {
            Frame frame = this.FrameManager.GetFrameByString(frameName);

            frame.Navigate(targetType); // TODO NO DEFAULT NAVIGATION!!!

            this.SetHistoryCurrentNode(this.History.FrameNavigationHistory.Last);

            this.RaizeOnNavigated(PagesManager.GetTypeOfPageByString(targetType.FullName), frame);
        }
        /// <summary>
        /// Navigates frame to type without adding to history
        /// </summary>
        public void NavigateFrameSilent(string frameName, HistoryRecord historyRecord)
        {
            Frame frame = this.FrameManager.GetFrameByString(frameName);

            Type pageType = PagesManager.GetTypeOfPageByString(historyRecord.PageFullName);

            frame.Navigate(pageType); // TODO NO DEFAULT NAVIGATION!!!

            this.SetHistoryCurrentNode(historyRecord);

            this.RaizeOnNavigated(PagesManager.GetTypeOfPageByString(pageType.FullName), frame);
        }

        #endregion

        /// <summary>
        /// Navigates forward frame according to the history
        /// </summary>
        public void NavigateForward(string frameName)
        {
            string target = this.GetNextHistoryRecord().PageFullName;
            Frame frame = this.FrameManager.GetFrameByString(frameName);
            Type targetPage = PagesManager.GetTypeOfPageByString(target);

            frame.Navigate(targetPage); // TODO NO DAFAULT NAVIGATION!!!
            this.RaizeOnNavigated(targetPage, frame);
        }

        /// <summary>
        /// Navigates backward frame according to the history
        /// </summary>
        public void NavigateBack(string frameName)
        {
            string target = this.GetPreviousHistoryRecord().PageFullName;
            Frame frame = this.FrameManager.GetFrameByString(frameName);
            Type targetPage = PagesManager.GetTypeOfPageByString(target);



            if (frame.SourcePageType.FullName == target)
            {
                frame.Navigate(targetPage);
                frame.BackStack.RemoveAt(frame.BackStack.Count - 1);
                
            }
            else
            {
                frame.Navigate(targetPage);
            }

            this.RaizeOnNavigated(targetPage, frame);


        }

        #endregion

        #region History Navigation Methods

        /// <summary>
        /// Goes to the next history record(gets value of it)
        /// </summary>
        /// <returns>thevalue of the next record</returns>
        public HistoryRecord GetNextHistoryRecord()
        {
            HistoryRecord result = null;

            if (this.CanNavigateForward())
            {
                this.History.CurrentNode = this.History.CurrentNode.Next;
                result = this.History.CurrentNode.Value;
            }

            return result;
        }
        /// <summary>
        /// Goes to the previous history record(increases current index and gets value of it)
        /// </summary>
        /// <returns>the value of the previous record</returns>
        public HistoryRecord GetPreviousHistoryRecord()
        {
            HistoryRecord result = null;

            if (this.CanNavigateBack())
            {
                this.History.CurrentNode = this.History.CurrentNode.Previous;
                result = this.History.GetCurrent();
            }

            return result;
        }
        /// <summary>
        /// Navigates to top 
        /// </summary>
        /// <returns></returns> 
        public HistoryRecord GetTopHistoryItem()
        {
            this.History.CurrentNode = this.History.FrameNavigationHistory.Last;
            return this.History.CurrentNode.Value;
        }
        /// <summary>
        /// sets current item to th elast navigated item
        /// </summary>
        /// <param name="historyRecord">navigation target history record full name</param>
        /// <returns></returns> 
        public bool SetHistoryCurrentNode(HistoryRecord historyRecord)
        {
            bool resultToReturn = false;

            LinkedListNode<HistoryRecord> result = this.History.FrameNavigationHistory
                 .Find(this.History.FrameNavigationHistory
                 .FirstOrDefault(t => t == historyRecord));

            if (result != null)
            {
                this.SetHistoryCurrentNode(result);
                resultToReturn = true;
            }
            return resultToReturn;
        }
        public void SetHistoryCurrentNode(LinkedListNode<HistoryRecord> node)
        {
                this.History.CurrentNode = node;
        }

        #endregion

        #region CanNavigate
        public bool CanNavigateForward() => this.History.CurrentNode?.Next != null;
        /// <summary>
        /// Gets value if navigation backward in nested frame is possible
        /// </summary>
        /// <returns></returns>
        public bool CanNavigateBack() => this.History.CurrentNode?.Previous != null;
        #endregion

        #endregion

        #region HelpMethods

        /// <summary>
        /// Initialization of pages of pages manager
        /// </summary>
        /// <param name="pages">list of pages by which to initialize</param>
        public static void InitializePageTypes(Type[] pages) => PagesManager.InitializePages(pages);

        /// <summary>
        /// Method which raises on navigeted
        /// </summary>
        /// <param name="page">navigated page (what)</param>
        /// <param name="frame">parrent frame (where)</param>
        private void RaizeOnNavigated(Type pageType, Frame frame) =>
            this.NavigationEventHandler?.OnNavigated(pageType, frame);
        #endregion
    }
}