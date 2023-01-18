using GoodPass.Activation;
using GoodPass.Contracts.Services;
using GoodPass.Models;
using GoodPass.Notifications;
using GoodPass.Services;
using GoodPass.ViewModels;
using GoodPass.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

namespace GoodPass;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging

    public IHost Host
    {
        get;
    }

    /*MasterKey加密数组*/
    public static int[]? EncryptBase;
    public static int[]? MKBase;
    /*End MasterKey加密数组*/

    /*数据成员*/
    public static GPManager DataManager;

    public static ListDetailsViewModel ListDetailsVM;
    /*End 数据成员*/

    /*App状态区*/
    private static bool LockConsition
    {
        get; set;
    }

    private static bool InSettingsPage
    {
        get; set;
    }

    public static bool App_IsLock() => LockConsition;//true为锁定状态，false为解锁状态

    public static void App_UnLock() => LockConsition = false;

    public static void App_Lock() => LockConsition = true;

    public static bool IsInSettingsPage() => InSettingsPage;

    public static void GoInSettingsPage() => InSettingsPage = true;

    public static void LeftSettingsPage() => InSettingsPage = false;
    /*App 状态区结束*/

    public static WindowEx MainWindow { get; } = new MainWindow();

    public App()
    {
        InitializeComponent();
        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers
            services.AddTransient<IActivationHandler, AppNotificationActivationHandler>();

            // Services
            services.AddSingleton<IAppNotificationService, AppNotificationService>();
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IMaterKeyService, MasterKeyService>();
            services.AddSingleton<MasterKeyService>();
            services.AddSingleton<GoodPassSHAServices>();
            services.AddSingleton<GoodPassCryptographicServices>();
            services.AddSingleton<GoodPassDataService>();
            services.AddSingleton<ISampleDataService, SampleDataService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<ListDetailsViewModel>();

            // Views and ViewModels
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
            services.AddTransient<ListDetailsViewModel>();
            services.AddTransient<ListDetailsPage>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainPage>();
            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();
            services.AddTransient<GPDialog2>();
            services.AddTransient<SetMKDialog>();

            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
        }).
        Build();

        /*禁止在类中初始化static成员*/

        App.GetService<IAppNotificationService>().Initialize();

        UnhandledException += App_UnhandledException;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        //App.GetService<IAppNotificationService>().Show(string.Format("AppNotificationSamplePayload".GetLocalized(), AppContext.BaseDirectory));

        await App.GetService<IActivationService>().ActivateAsync(args);
    }
}
