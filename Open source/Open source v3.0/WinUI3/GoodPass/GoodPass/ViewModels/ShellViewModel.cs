using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GoodPass.Contracts.Services;
using GoodPass.ViewModels;
using GoodPass.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.ApplicationModel.Store;
using Windows.Globalization;
using Windows.UI.ViewManagement;

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

    private void OnMenuFileExit()
    {
        //ToDo:添加退出程序前文件保护和数据加密机制
        OnMenuFileLock();
        Application.Current.Exit();
    }

    private void OnMenuSettings()
    {
        App.GoInSettingsPage(); 
        NavigationService.NavigateTo(typeof(SettingsViewModel).FullName!);
    }

    private void OnMenuViewsListDetails() => NavigationService.NavigateTo(typeof(ListDetailsViewModel).FullName!);

    private void OnMenuViewsMain() => NavigationService.NavigateTo(typeof(MainViewModel).FullName!);

    private void OnMenuFileLock()
    {
        //ToDo:添加文件保存等锁定数据防护操作
        App.App_Lock();
        App.LeftSettingsPage();
        NavigationService.NavigateTo(typeof(MainViewModel).FullName!);
    }

    public void GoBack()
    {
        if (!App.App_IsLock())
        {
            NavigationService.GoBack();
        }
        else if (App.IsInSettingsPage() == true)
        {
            App.LeftSettingsPage();
            NavigationService.GoBack();
        }
    }
}
