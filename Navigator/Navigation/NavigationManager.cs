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

        #region Navigate Frame
        /// <summary>
        /// Navigate frame to type
        /// </summary>
        public void NavigateFrame(Frame frame, Type targetType)
        {
            Frame f = frame;

            frame.Navigate(targetType); // TODO NO DEFAULT NAVIGATION!!!

            this.History.ClearAfter(this.History.GetCurrent());

            this.History.Add(f, targetType);

            this.SetHistoryCurrentNode(this.History.FrameNavigationHistory.Last);

            this.RaizeOnNavigated(PagesManager.GetTypeOfPageByString(targetType.FullName), frame);
        }   // Main Navigate Method

        /// <summary>
        /// Navigate frame to type by string
        /// </summary>
        public void NavigateFrame(string frame, string pageName)
        {
            Type targetType = PagesManager.GetTypeOfPageByString(pageName);

            this.NavigateFrame(frame, targetType);
        }
        /// <summary>
        /// Navigate frame to type
        /// </summary>
        public void NavigateFrame(string frameName, Type targetType)
        {
            Frame frame = this.FrameManager.GetFrameByString(frameName);

            this.NavigateFrame(frame, targetType);
        }
        public void NavigateFrame(Frame frame, string pageName)
        {
            Type targetType = PagesManager.GetTypeOfPageByString(pageName);

            this.NavigateFrame(frame, targetType);
        }
        /// <summary>
        /// Navigate frame to type
        /// </summary>
        public void NavigateFrame(HistoryRecord historyRecord)
        {
            Frame frame = historyRecord.ParrentFrame;

            this.NavigateFrame(frame,historyRecord.PageFullName);
        }
        #endregion

        #region Navigate Frame Silent
        /// <summary>
        /// Navigates frame to type without adding to history
        /// </summary>
        public void NavigateFrameSilent(Frame frame, Type targetType)
        {
            frame.Navigate(targetType); // TODO NO DEFAULT NAVIGATION!!!
            
            this.RaizeOnNavigated(PagesManager.GetTypeOfPageByString(targetType.FullName), frame);
        }   //Main Navigate Silent Method

        /// <summary>
        /// Navigates frame to type without adding to history
        /// </summary>
        public void NavigateFrameSilent(string frameName, Type targetType)
        {
            Frame frame = this.FrameManager.GetFrameByString(frameName);

            this.NavigateFrameSilent(frame,targetType);
        }
        /// <summary>
        /// Navigates frame to type without adding to history
        /// </summary>
        public void NavigateFrameSilent(string frameName, string pageName)
        {
            this.NavigateFrameSilent(frameName, PagesManager.GetTypeOfPageByString(pageName)); // TODO NO DEFAULT NAVIGATION!!!
        }
        /// <summary>
        /// Navigates frame to type without adding to history
        /// </summary>
        public void NavigateFrameSilentByHistoryRecord(HistoryRecord historyRecord)
        {
            Type pageType = PagesManager.GetTypeOfPageByString(historyRecord.PageFullName);

            Frame parrentFrame = historyRecord.ParrentFrame;

            this.SetHistoryCurrentNode(historyRecord);

            this.NavigateFrameSilent(parrentFrame, pageType);
        }
        #endregion

        #region Navigate Back & Forward
        // Forward
        /// <summary>
        /// Navigates forward frame according to the history
        /// </summary>
        public void NavigateForward(Frame frame)
        {
            HistoryRecord historyRecord = this.GoNextHistoryRecord();
            Type targetPage = PagesManager.GetTypeOfPageByString(historyRecord.PageFullName);

            frame.Navigate(targetPage); // TODO NO DAFAULT NAVIGATION!!!
            this.RaizeOnNavigated(targetPage, frame);
        }   // Main navigate forward method
        /// <summary>
        /// Navigates forward frame according to the history
        /// </summary>
        public void NavigateForward(string frameName)
        {
            Frame frame = this.FrameManager.GetFrameByString(frameName);

            this.NavigateForward(frame);
        }
        public void NavigateForward()
        {
            HistoryRecord historyRecord = this.GetNextHistoryRecord();
            Frame frame = historyRecord.ParrentFrame;

            this.NavigateForward(frame);
        }

        // Back
        /// <summary>
        /// Navigates backward frame according to the history to specified frame
        /// </summary>
        public void NavigateBack(Frame frame)
        {
            Frame parrentFrame = this.GetCurrentHistoryRecord().ParrentFrame; 
            Type targetPage = PagesManager.GetTypeOfPageByString(this.GoPreviousHistoryRecord().PageFullName);
            Frame onNavigatedFrame;


            if (parrentFrame != frame)
            {
                parrentFrame.GoBack(); // Navigate from outer(parrentFrame)
                onNavigatedFrame = parrentFrame;
            }
            else
            {  
                
                frame.Navigate(targetPage); // Navigate incide frame
                onNavigatedFrame = frame;
            }

            this.RaizeOnNavigated(targetPage, onNavigatedFrame);


        }   // Main navigate backward method
        /// <summary>
        /// Navigates backward frame according to the history to specified frame
        /// </summary>
        public void NavigateBack(string frameName)
        {
            Frame frame = this.FrameManager.GetFrameByString(frameName);
            this.NavigateBack(frame);
        }
        /// <summary>
        /// Navigates backward frame according to the history
        /// </summary>
        public void NavigateBack()
        {
            HistoryRecord historyRecord = this.GetPreviousHistoryRecord();
            Frame frame = historyRecord.ParrentFrame;

            this.NavigateBack(frame);
        }
        #endregion

        #endregion

        #region History Navigation Methods

        /// <summary>
        /// Gets current history item
        /// </summary>
        /// <returns></returns> 
        public HistoryRecord GetCurrentHistoryRecord()
        {
            return this.History.GetCurrent();
        }
        /// <summary>
        /// Gets next history item
        /// </summary>
        /// <returns></returns>
        public HistoryRecord GetNextHistoryRecord()
        {
            HistoryRecord result = null;

            if (this.CanNavigateForward())
            {
                result = this.History.CurrentNode.Next.Value;
            }

            return result;
        }
        /// <summary>
        /// Gets previous history item
        /// </summary>
        /// <returns>the value of the previous record</returns>
        public HistoryRecord GetPreviousHistoryRecord()
        {
            HistoryRecord result = null;

            if (this.CanNavigateBack())
            {
                result = this.History.CurrentNode.Previous.Value;
            }

            return result;
        }
        /// <summary>
        /// Gets top history item
        /// </summary>
        /// <returns></returns> 
        public HistoryRecord GetTopHistoryRecord()
        {
            return this.History.FrameNavigationHistory.Last.Value;
        }


        /// <summary>
        /// Goes to the next history record(gets value of it)
        /// </summary>
        /// <returns>thevalue of the next record</returns>
        public HistoryRecord GoNextHistoryRecord()
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
        /// Goes to the previous history record(gets value of item)
        /// </summary>
        /// <returns>the value of the previous record</returns>
        public HistoryRecord GoPreviousHistoryRecord()
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
        /// Goes to top of history
        /// </summary>
        /// <returns>value of the top item</returns> 
        public HistoryRecord GoTopHistoryItem()
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
        /// <param name="pageType">navigated page type (what)</param>
        /// <param name="frame">parrent frame (where)</param>
        private void RaizeOnNavigated(Type pageType, Frame frame) =>
            this.NavigationEventHandler?.OnNavigated(pageType, frame);
        #endregion
    }
}