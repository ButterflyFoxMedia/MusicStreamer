using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using DotNetEnv;
using System.IO;
using System.Threading.Tasks;

namespace MusicStreamer
{

    public static class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {

            string CacheDirectory = FileSystem.Current.CacheDirectory;
            string AppDirectory = FileSystem.Current.AppDataDirectory;

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureLifecycleEvents(events =>
                {
#if ANDROID
                    events.AddAndroid(android => android
                        .OnActivityResult((activity, requestCode, resultCode, data) => LogEvent(nameof(AndroidLifecycle.OnActivityResult), requestCode.ToString()))
                        .OnStart((activity) => LogEvent(nameof(AndroidLifecycle.OnStart)))
                        .OnCreate((activity, bundle) => LogEvent(nameof(AndroidLifecycle.OnCreate)))
                        .OnBackPressed((activity) => LogEvent(nameof(AndroidLifecycle.OnBackPressed)) && false)
                        .OnStop((activity) => LogEvent(nameof(AndroidLifecycle.OnStop)))
                        );
#endif
#if IOS || MACCATALYST
                    events.AddiOS(ios => ios
                        .OnActivated((app) => LogEvent(nameof(iOSLifecycle.OnActivated)))
                        .OnResignActivation((app) => LogEvent(nameof(iOSLifecycle.OnResignActivation)))
                        .DidEnterBackground((app) => LogEvent(nameof(iOSLifecycle.DidEnterBackground)))
                        .WillTerminate((app) => LogEvent(nameof(iOSLifecycle.WillTerminate)))
                        .FinishedLaunching((app, bundle) => LogEvent(nameof(iOSLifecycle.FinishedLaunching)))
                        
                    );
#endif
                    static bool LogEvent(string eventName, string type = "Not Provided")
                    {
                        
                        try
                        {
                            string LogFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Record.log");

                            using (StreamWriter writer = File.AppendText(LogFilePath))
                            {
                                writer.WriteLineAsync($"{DateTime.Now.ToString()}:TYPE:{type}");
                                writer.WriteLineAsync($"{DateTime.Now.ToString()}:EVENT:{eventName}");
                            }

                        }
                        catch(Exception ex)
                        {
                            //TODO: Handle Exception Not Written To File
                        }

                        return true;
                    }
                }
                    )
                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Pelagiad.ttf", "Pelagiad");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static class CredentialManager
        {
            public static async Task<bool>  CheckCredentials()
            {
                string? token = await SecureStorage.Default.GetAsync("api_token"); //Mark string as nullable

                //if token is null, go through login
                while (token == null)
                {
                    //Launch LoginPage as Modal (Modal Call is handled in LoginPage.XAML)
                    await Shell.Current.GoToAsync("LoginPage");

                    //Set token before testing iteration condition
                    token = await SecureStorage.Default.GetAsync("api_token");
                } //if token is not null, then escape loop

                return true;
            }
        }
    }
}
