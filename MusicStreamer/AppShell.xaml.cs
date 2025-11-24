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
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
    }
}
