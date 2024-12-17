using Microsoft.Extensions.Logging;
using AFStudiumAPIClient.IoC;
using AFStudiumAPIClient;

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
            builder.Services.AddSingleton<SqliteConnectionBase>();

            //builder.
#if DEBUG
            builder.Logging.AddDebug();

#endif

            return builder.Build();
        }
    }
}
