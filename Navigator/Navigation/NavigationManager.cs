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
            Type t = PagesManager.GetPageByString(pageName).GetType();

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

            this.History.Add(targetType);

            this.GetHistoryItem(targetType.FullName);

            this.History.ClearAfter(targetType);

            this.RaizeOnNavigated((Page)Activator.CreateInstance(targetType), frame);
        }

        /// <summary>
        /// Navigates frame to type without adding to history
        /// </summary>
        public void NavigateFrameSilent(string frameName, Type targetType)
        {
            Frame frame = this.FrameManager.GetFrameByString(frameName);

            frame.Navigate(targetType); // TODO NO DEFAULT NAVIGATION!!!

            this.RaizeOnNavigated((Page)Activator.CreateInstance(targetType), frame);
        }
        #endregion

        /// <summary>
        /// Navigates forward frame according to the history
        /// </summary>
        public void NavigateForward(string frameName)
        {
            string target = this.GetNextHistoryRecord().FullPageName;
            Frame frame = this.FrameManager.GetFrameByString(frameName);
            Page targetPage = PagesManager.GetPageByString(target);

            frame.Navigate(targetPage.GetType()); // TODO NO DAFAULT NAVIGATION!!!
            this.RaizeOnNavigated(targetPage, frame);
        }

        /// <summary>
        /// Navigates backward frame according to the history
        /// </summary>
        public void NavigateBack(string frameName)
        {
            string target = this.GetPreviousHistoryRecord().FullPageName;
            Frame frame = this.FrameManager.GetFrameByString(frameName);
            Page targetPage = PagesManager.GetPageByString(target);



            if (frame.SourcePageType.FullName == target)
            {
                frame.Navigate(targetPage.GetType());
                frame.BackStack.RemoveAt(frame.BackStack.Count - 1);
                this.RaizeOnNavigated(targetPage, frame);
            }
            else
            {
                frame.Navigate(targetPage.GetType());
            }
           

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
                result = this.History.GetCurrentPageType();
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
        /// <param name="pageFullName">navigation target page full name</param>
        /// <returns></returns> 
        public bool GetHistoryItem(string pageFullName)
        {
            bool resultToReturn = false;

            LinkedListNode<HistoryRecord> result = this.History.FrameNavigationHistory
                 .Find(this.History.FrameNavigationHistory
                 .FirstOrDefault(t => t.FullPageName == pageFullName));

            if (result != null)
            {
                this.History.CurrentNode = result;
                resultToReturn = true;
            }
            return resultToReturn;
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
        public static void InitializePages(Page[] pages) => PagesManager.InitializePages(pages);

        /// <summary>
        /// Method which raises on navigeted
        /// </summary>
        /// <param name="page">navigated page (what)</param>
        /// <param name="frame">parrent frame (where)</param>
        private void RaizeOnNavigated(Page page, Frame frame) =>
            this.NavigationEventHandler?.OnNavigated(page, frame);
        #endregion
    }
}