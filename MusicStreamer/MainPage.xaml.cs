using MusicStreamer.Pages;
using System.Threading.Tasks;

namespace MusicStreamer
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            CheckSavedCredentials();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        static async Task<bool> CheckSavedCredentials()
        {
            string credPath = Path.Combine(FileSystem.Current.CacheDirectory, ".env");
            if (File.Exists(credPath))
            {

            }
            else
            {
                //Launch LoginPage as Modal
                await Shell.Current.GoToAsync("//LoginPage");
            }
            //TODO: Check for Credential File, if exists - load token, if not exists, prompt for login to obtain token
            return true;
        }
    }
}
