using System.Windows;
using ClinicManager.Win.Net.Http;
using ClinicManager.Win.Presentation.Mvvm;
using ClinicManager.Win.ViewModels;
using ClinicManager.Win.Views;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;

namespace ClinicManager.Win;

public partial class App : PrismApplication
{
    /// <summary>
    /// Prism's DryIoc container with the Microsoft.Extensions.DependencyInjection adapter,
    /// so anything registered on an IServiceCollection (HttpClientFactory, logging, options...) is resolvable by Prism too.
    /// </summary>
    protected override IContainerExtension CreateContainerExtension()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);

        var container = new Container(CreateContainerRules())
            .WithDependencyInjectionAdapter(services);

        return new DryIocContainerExtension(container);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient<IApiClient, ApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:5001/");
        });
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Windows
        containerRegistry.Register<LoginView>();
        containerRegistry.Register<ShellWindow>();

        // Region views (navigation names == type names, see ViewNames)
        containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
        containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
        containerRegistry.RegisterForNavigation<DashboardView, DashboardViewModel>();
    }

    protected override Window CreateShell() => Container.Resolve<ShellWindow>();

    protected override void OnInitialized()
    {
        // Login is a modal window shown before the shell.
        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        var login = Container.Resolve<LoginView>();
        if (login.ShowDialog() != true)
        {
            Shutdown();
            return;
        }

        ShutdownMode = ShutdownMode.OnMainWindowClose;

        var regions = Container.Resolve<IRegionManager>();
        regions.RegisterViewWithRegion(RegionNames.Shell, typeof(MainView));
        regions.RegisterViewWithRegion(RegionNames.Content, typeof(HomeView));

        base.OnInitialized();
    }
}
