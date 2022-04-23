using CommunityToolkit.Maui;
using OrleansDemo.Domain;

namespace OrleansDemo.MauiNativeApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.RegisterCommonDependencies();
            builder.Services.AddTransient<Index>();

            return builder.Build();
        }
    }
}