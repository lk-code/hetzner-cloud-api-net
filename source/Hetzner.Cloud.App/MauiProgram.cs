using System.Reflection;
using FluentMAUI.Configuration;
using Microsoft.Extensions.Logging;

namespace Hetzner.Cloud.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();
        
        builder.Services.AddHetznerCloud();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        
        builder.UseFluentConfiguration(options =>
        {
            options.LoadAppsettingsFrom = Assembly.GetExecutingAssembly();
        });

        return builder.Build();
    }
}