using GoodPass.Activation;
using GoodPass.Contracts.Services;
using GoodPass.Dialogs;
using GoodPass.Models;
using GoodPass.Notifications;
using GoodPass.Services;
using GoodPass.Strings;
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

    /// <summary>
    /// 加密基数组
    /// </summary>
    public static int[]? EncryptBase;
    /// <summary>
    /// 主密码基数组
    /// </summary>
    public static int[]? MKBase;

    public static byte[] AESKey;

    public static byte[] AESIV;

    /// <summary>
    /// 数据管理成员
    /// </summary>
    public static GPManager DataManager;

    /// <summary>
    /// 公共的ListDetailViewModel
    /// </summary>
    public static ListDetailsViewModel ListDetailsVM;

    /// <summary>
    /// App锁定情况
    /// </summary>
    private static bool LockConsition
    {
        get; set;
    }

    /// <summary>
    /// App设置页情况
    /// </summary>
    private static bool InSettingsPage
    {
        get; set;
    }

    /// <summary>
    /// 多语言字符串资源
    /// </summary>
    public static UIStrings UIStrings
    {
        get; set;
    }

    /// <summary>
    /// 获取App锁定情况
    /// </summary>
    /// <returns>true为锁定状态，false为解锁状态</returns>
    public static bool App_IsLock() => LockConsition;//

    /// <summary>
    /// 解锁App接口
    /// </summary>
    public static void App_UnLock() => LockConsition = false;

    /// <summary>
    /// 锁定App接口
    /// </summary>
    public static void App_Lock() => LockConsition = true;

    /// <summary>
    /// 获取App设置页情况
    /// </summary>
    /// <returns>true为在设置页，false为不在设置页</returns>
    public static bool IsInSettingsPage() => InSettingsPage;

    /// <summary>
    /// 进入设置页状态更改接口
    /// </summary>
    public static void GoInSettingsPage() => InSettingsPage = true;

    /// <summary>
    /// 离开设置页状态更改接口
    /// </summary>
    public static void LeftSettingsPage() => InSettingsPage = false;

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
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<ListDetailsViewModel>();
            services.AddSingleton<MultilingualStringsServices>();

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
