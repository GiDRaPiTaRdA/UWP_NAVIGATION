using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Navigator.Navigation.History;
using Navigator.NavigationEventsHandler;
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
        /// Loaded pages
        /// </summary>
        public Page[] Pages { get; private set; }
        #endregion

        public NavigationManager()
        {
            this.History =  new NavigationHistory();
            this.NavigationEventHandler = new NavigationEventHandler();
        }

        #region ________________________PageNavigation_________________________

        #region Navigation

        /// <summary>
        /// Navigate frame to type
        /// </summary>
        public void NavigateFrame(Frame frame,Type targetType)
        {
             frame.Navigate(targetType,null,new CommonNavigationTransitionInfo()); // TODO NO DEFAULT NAVIGATION!!!

            //frame.SourcePageType = targetType;

            this.History.Add(targetType);

            this.History.ClearAfter(targetType);

            this.GoHistoryItem(targetType);

            this.RaizeOnNavigated(targetType, frame);
        }

        /// <summary>
        /// Navigates frame to type without adding to history
        /// </summary>
        public void NavigateFrameSilent(Frame frame, Type targetType)
        {
            frame.Navigate(targetType); // TODO NO DEFAULT NAVIGATION!!!

            this.History.ClearAfter(targetType);

            this.RaizeOnNavigated(targetType, frame);
        }

        /// <summary>
        /// Navigates forward frame according to the history
        /// </summary>
        public void NavigateForward(Frame frame)
        {
            Type target = this.GoNextHistoryRecord().PageType;
            if (target != null)
            {

                frame.Navigate(target); // TODO NO DAFAULT NAVIGATION!!!
                this.RaizeOnNavigated(target, frame);
            }
        }

        /// <summary>
        /// Navigates backward frame according to the history
        /// </summary>
        public void NavigateBack(Frame frameCurrent,Frame frameNested= null)
        {
            Type target = this.GoPreviousHistoryRecord().PageType;
            if (target != null)
            {
                if (frameCurrent.SourcePageType == target)
                {
                    frameCurrent.Navigate(target);
                    frameCurrent.BackStack.RemoveAt(frameCurrent.BackStack.Count - 1);
                }
                else
                    frameNested.Navigate(target); // TODO NO DAFAULT NAVIGATION!!!

                this.RaizeOnNavigated(target, frameCurrent);
            }
        }

        #endregion

        #region History Navigation Methods

        /// <summary>
        /// Goes to the next history record(increases current index and gets value of it)
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
        /// Goes to the previous history record(increases current index and gets value of it)
        /// </summary>
        /// <returns>the value of the previous record</returns>
        public HistoryRecord GoPreviousHistoryRecord()
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
        public HistoryRecord GoTopHistoryItem()
        {
            this.History.CurrentNode = this.History.FrameNavigationHistory.Last;
            return this.History.CurrentNode.Value;
        }
        /// <summary>
        /// sets current item to th elast navigated item
        /// </summary>
        /// <param name="type">typeof navigation target</param>
        /// <returns></returns> 
        public bool GoHistoryItem(Type type)
        {
            bool resultToReturn = false;

            LinkedListNode<HistoryRecord> result = this.History.FrameNavigationHistory
                 .Find(this.History.FrameNavigationHistory
                 .FirstOrDefault(t => t.PageType.FullName == type.FullName));

            if (result != null)
            {
                this.History.CurrentNode = result;
                resultToReturn = true;
            }
            return resultToReturn;
        }
        #endregion

        #region CanNavigate
        public bool CanNavigateForward() => this.History.CurrentNode.Next!=null;
        /// <summary>
        /// Gets value if navigation backward in nested frame is possible
        /// </summary>
        /// <returns></returns>
        public bool CanNavigateBack() => this.History.CurrentNode.Previous != null;
        #endregion

        #endregion

        #region HelpMethods
        /// <summary>
        /// Method which raises on navigeted
        /// </summary>
        /// <param name="pageType">page type</param>
        /// <param name="frame">frame</param>
        private void RaizeOnNavigated(Type pageType, Frame frame) =>
             this.NavigationEventHandler?.OnNavigated(pageType);

        public void InitializePages(Page[] pages)
        {
            this.Pages = pages;
        }
        #endregion
    }
}