namespace SRBD_UWP.ViewModel
{
    public class WrappingViewViewModel
    {
        public string FrameName => Static.NavigationFrames.WrapperFrame.ToString();

        public WrappingViewViewModel()
        {
            Navigator.Navigation.NavigationManager.Instance.NavigateFrame(
               Static.NavigationFrames.WrapperFrame.ToString(),
               "MainMenu.View.LoginPage"
               );
        }
    }
}