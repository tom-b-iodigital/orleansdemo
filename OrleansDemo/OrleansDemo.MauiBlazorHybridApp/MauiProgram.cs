using Microsoft.AspNetCore.Components.WebView.Maui;
using OrleansDemo.Domain;
using OrleansDemo.MauiBlazorHybridApp.Data;

namespace OrleansDemo.MauiBlazorHybridApp
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
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.RegisterCommonDependencies();

            return builder.Build();
        }
    }
}