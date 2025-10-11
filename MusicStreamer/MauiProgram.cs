using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace MusicStreamer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
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
                    );
#endif
                    static bool LogEvent(string eventName, string type = "Not Provided")
                    {
                        //TODO: log to logfile
                        return true;
                    }
                }
                    )
                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public class GlobalDataInfo()
        {
            string _username = string.Empty;
            string _password = string.Empty;
            string _usertoken = string.Empty;

            public string Username
            {
                get
                { 
                    return _username; 
                }
                private set
                {
                    _username = value;
                }
            }
            public string Password
            {
                get
                {
                    return _password;
                    //Return hashed password and only unhash before sending to Emby
                }
                private set
                {
                    _password = value;
                    //Always hash before sending password
                }
            }

            public string Usertoken
            {
                get 
                {
                    return _usertoken;
                    //Return hashed usertoken and only unhash before sending to Emby
                }
                private set
                {
                    _usertoken = value;
                    //Always hash before sending usertoken
                }
            }
        }
    }
}
