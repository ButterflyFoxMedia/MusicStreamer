using MusicStreamer.Pages;

namespace MusicStreamer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Register Routes

            //Login Page
            Routing.RegisterRoute("EmbyLogin", typeof(LoginPage));
            Routing.RegisterRoute("PlexLogin", typeof(LoginPage));
        }
    }
}
