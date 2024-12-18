using Microsoft.Extensions.Logging;
using AFStudiumAPIClient.IoC;
using AFStudiumAPIClient;
using AFStudiumApp.ViewModels;


namespace AFStudiumApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddAFStudiumAPIClientService(x => x.ApiBaseAddress = "http://10.0.2.2:5000");
           /* builder.Services.AddSingleton<AFStudiumAPIClientService>();
            builder.Services.AddHttpClient<AFStudiumAPIClientService>(client =>
            {
                client.BaseAddress = new Uri("http://10.0.2.2:5214/");
            });*/
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<ViewModelBase>();
            builder.Services.AddTransient<ModulesViewModel>();
            builder.Services.AddTransient<ModulesPage>();
            builder.Services.AddTransient<AddModulPage>();
            builder.Services.AddTransient<AddEventPage>();
            builder.Services.AddTransient<AboutModulPage>();

            //builder.
#if DEBUG
            builder.Logging.AddDebug();
            //builder.Services.AddSingleton<SqliteConnectionBase>();
#endif

            return builder.Build();
        }
    }
}
