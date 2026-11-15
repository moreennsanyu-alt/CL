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
using System.Runtime.CompilerServices;

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
        services.AddCmConfig(c =>
        {
            c.ApplicationName = "MyApp";
            c.Environment = "Staging";
            c.Port = 9092;
            c.EnableLogging = false;
            c.ConnectionString = "Server=...;";
        });
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

      protected override Rules CreateContainerRules()
    {
        return Rules.Default.WithConcreteTypeDynamicRegistrations(reuse: Reuse.Transient)
                        .With(Made.Of(FactoryMethod.ConstructorWithResolvableArguments))
                        .WithFuncAndLazyWithoutRegistration()
                        .WithTrackingDisposableTransients()
                        //.WithoutFastExpressionCompiler()
                        .WithFactorySelector(Rules.SelectLastRegisteredFactory());
    }

    // App.xaml.cs
    protected override void ConfigureModuleCatalog(IModuleCatalog catalog)
    {
        var moduleTypes = typeof(CMApplication).Assembly
            .GetTypes()
            .Where(t => typeof(IModule).IsAssignableFrom(t)
                    && t is { IsAbstract: false, IsInterface: false });

        foreach (var type in moduleTypes)
            catalog.AddModule(type);   // WhenAvailable by default
    }
    protected override void ConfigureViewModelLocator()
    {
        ViewModelLocationProvider.SetDefaultViewModelFactory((viewModelType) => 
        {
            return Container.Resolve(viewModelType);
        });
        
        ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
        {
            var viewName = viewType.FullName;
            var assemblyName = viewType.Assembly.FullName;
            string viewModelName;

            if (viewName.EndsWith("Window"))
            {
                var baseName = viewName.Substring(0, viewName.Length - "Window".Length);
                viewModelName = ResolveFirst(assemblyName, baseName + "ViewModel", baseName + "WindowViewModel");
            }
            else if (viewName.EndsWith("Page"))
            {
                var baseName = viewName.Substring(0, viewName.Length - "Page".Length);
                viewModelName = ResolveFirst(assemblyName, baseName + "PageViewModel", baseName + "ViewModel");
            }
            else if (viewName.EndsWith("Control"))
            {
                var baseName = viewName.Substring(0, viewName.Length - "Control".Length);
                viewModelName = ResolveFirst(assemblyName, baseName + "ViewModel", baseName + "ControlViewModel");
            }
            else if (viewName.EndsWith("View"))
            {
                var baseName = viewName.Substring(0, viewName.Length - "View".Length);
                viewModelName = baseName + "ViewModel";
            }
            else
            {
                viewModelName = viewName + "ViewModel";
            }

            return Type.GetType($"{viewModelName}, {assemblyName}");
        });
    }

    private static string ResolveFirst(string assemblyName, string primaryName, string fallbackName)
    {
        return Type.GetType($"{primaryName}, {assemblyName}") != null ? primaryName : fallbackName;
    }
}
