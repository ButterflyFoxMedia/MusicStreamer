using MusicStreamer.Pages;
using System.Threading.Tasks;
using static MusicStreamer.MauiProgram.CredentialManager;

namespace MusicStreamer
{
    public partial class MainPage : ContentPage
    {
        
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Dispatcher.DispatchAsync(async () =>
            {
                _ = await CheckCredentials();
            });
            //TODO: If Server securestorage exists, load media page, else load Servers page
        }
        
    }
}
