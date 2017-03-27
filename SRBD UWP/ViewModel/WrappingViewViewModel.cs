namespace SRBD_UWP.ViewModel
{
    public class WrappingViewViewModel
    {
        public string FrameName => Static.NavigationFrames.WrapperFrame;

        public WrappingViewViewModel()
        {
            Navigator.Navigation.NavigationManager.Instance.NavigateFrameSilent(
                this.FrameName,
               "MainMenu.View.LoginPage"
               );
        }
    }
}