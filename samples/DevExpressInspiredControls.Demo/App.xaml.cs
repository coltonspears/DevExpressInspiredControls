using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace DevExpressInspiredControls.Demo;

public partial class App : Application
{
    private ServiceProvider Services { get; } = ConfigureServices();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Services.GetRequiredService<MainWindow>().Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Services.Dispose();
        base.OnExit(e);
    }

    private static ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton(TimeProvider.System)
            .AddTransient<MainViewModel>()
            .AddTransient<MainWindow>()
            .BuildServiceProvider();
    }
}

