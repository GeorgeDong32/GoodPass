using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoodPass.Contracts.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;

namespace GoodPass.ViewModels;

public class ShellViewModel : ObservableRecipient
{
    private bool _isBackEnabled;

    public ICommand MenuFileExitCommand
    {
        get;
    }

    public ICommand MenuSettingsCommand
    {
        get;
    }

    public ICommand MenuViewsListDetailsCommand
    {
        get;
    }

    public ICommand MenuViewsMainCommand
    {
        get;
    }

    public ICommand GoBackCommand
    {
        get;
    }

    public INavigationService NavigationService
    {
        get;
    }

    public ICommand MenuFileLockCommand
    {
        get;
    }

    public bool IsBackEnabled
    {
        get => _isBackEnabled;
        set => SetProperty(ref _isBackEnabled, value);
    }

    public ShellViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;

        MenuFileExitCommand = new RelayCommand(OnMenuFileExit);
        MenuSettingsCommand = new RelayCommand(OnMenuSettings);
        MenuViewsListDetailsCommand = new RelayCommand(OnMenuViewsListDetails);
        MenuViewsMainCommand = new RelayCommand(OnMenuViewsMain);
        GoBackCommand = new RelayCommand(GoBack);
        MenuFileLockCommand = new RelayCommand(OnMenuFileLock);
    }

    private void OnNavigated(object sender, NavigationEventArgs e) => IsBackEnabled = NavigationService.CanGoBack;

    /// <summary>
    /// 点击MenuBar中退出按钮的操作实现
    /// </summary>
    private async void OnMenuFileExit()
    {
        //实现锁定并保存数据
        //保存到文件
        var dataPath = Path.Combine($"C:\\Users\\{Environment.UserName}\\AppData\\Local", "GoodPass", "GoodPassData.csv");
        if (App.DataManager != null)
            await App.DataManager.SaveToFileAsync(dataPath);
        //锁定
        App.App_Lock();
        App.LeftSettingsPage();
        Application.Current.Exit();
    }

    /// <summary>
    /// 点击MenuBar设置按钮的事件处理
    /// </summary>
    private void OnMenuSettings()
    {
        if (App.IsInSettingsPage())
        {
            GoBack();
        }
        else
        {
            App.GoInSettingsPage();
            NavigationService.NavigateTo(typeof(SettingsViewModel).FullName!);
        }
    }

    /// <summary>
    /// Navigate to ListDetail page
    /// </summary>
    private void OnMenuViewsListDetails() => NavigationService.NavigateTo(typeof(ListDetailsViewModel).FullName!);

    /// <summary>
    /// Navigate to main(login) page
    /// </summary>
    private void OnMenuViewsMain() => NavigationService.NavigateTo(typeof(MainViewModel).FullName!);

    /// <summary>
    /// 点击MenuBar锁定的事件处理
    /// </summary>
    private async void OnMenuFileLock()
    {
        //保存到文件
        var dataPath = Path.Combine($"C:\\Users\\{Environment.UserName}\\AppData\\Local", "GoodPass", "GoodPassData.csv");
        if (App.DataManager != null)
            await App.DataManager.SaveToFileAsync(dataPath);
        //锁定
        App.App_Lock();
        App.LeftSettingsPage();
        NavigationService.NavigateTo(typeof(MainViewModel).FullName!);
    }

    /// <summary>
    /// 导航返回的实现
    /// </summary>
    public void GoBack()
    {
        if (!App.App_IsLock() && !App.IsInSettingsPage())
        {
            NavigationService.GoBack();
        }
        else if (App.IsInSettingsPage())
        {
            App.LeftSettingsPage();
            NavigationService.GoBack();
        }
    }
}
