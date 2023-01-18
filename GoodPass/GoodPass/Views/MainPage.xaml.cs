using GoodPass.Services;
using GoodPass.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace GoodPass.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MasterKeyService MKS
    {
        get;
    }

    public MainPage()
    {
        App.App_Lock();
        ViewModel = App.GetService<MainViewModel>();
        MKS = App.GetService<MasterKeyService>();
        InitializeComponent();
    }

    private void Login_Check_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var passwordInput = Login_PssswordBox.Password;
        var MKCheck_Result = MKS.CheckMasterKey(passwordInput);
        //添加解锁逻辑
        if (MKCheck_Result == "pass")
        {
            App.App_UnLock();
            ViewModel.Login_UnLock();
        }
        else if (MKCheck_Result == "npass")
        {
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Background = new SolidColorBrush(Color.FromArgb(200, 255, 0, 0));//设置提示为红色
            Login_InfoBar.Message = "密码错误，请检查后重试！";//底部横幅提示
        }
        else if (MKCheck_Result == "error: not found")
        {
            //报错：MKConfig路径不存在
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Background = new SolidColorBrush(Color.FromArgb(200, 255, 0, 0));
            Login_InfoBar.Message = "配置文件不存在！";
            //To Do: 添加进入设置密码界面
            ShowSetMKDialog();
        }
        else if (MKCheck_Result == "error: data broken")
        {
            //报错：MKConfig数据损坏
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Background = new SolidColorBrush(Color.FromArgb(200, 255, 0, 0));
            Login_InfoBar.Message = "配置文件损坏，请修复！";
            //To Do: 添加进入重设密码界面
            ShowResetMKDialog();
        }
        else
        {
            //报错：未知错误
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Background = new SolidColorBrush(Color.FromArgb(200, 255, 0, 0));
            Login_InfoBar.Message = "未知错误！";
        }
    }

    private async void Login_Check_ClickAsync(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var passwordInput = Login_PssswordBox.Password;
        var MKCheck_Result = await MKS.CheckMasterKeyAsync(passwordInput);
        App.DataManager = new Models.GPManager();
        //添加解锁逻辑
        if (MKCheck_Result == "pass")
        {
            App.App_UnLock();
            ViewModel.Login_UnLock();
        }
        else if (MKCheck_Result == "npass")
        {
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Background = new SolidColorBrush(Color.FromArgb(120, 255, 0, 0));//设置提示为红色
            Login_InfoBar.Message = "密码错误，请检查后重试！";//底部横幅提示
        }
        else if (MKCheck_Result == "error: not found")
        {
            //报错：MKConfig路径不存在
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Background = new SolidColorBrush(Color.FromArgb(120, 255, 0, 0));
            Login_InfoBar.Message = "配置文件不存在！";
            //To Do: 添加进入设置密码界面
            ShowSetMKDialog();
        }
        else if (MKCheck_Result == "error: data broken")
        {
            //报错：MKConfig数据损坏
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Background = new SolidColorBrush(Color.FromArgb(120, 255, 0, 0));
            Login_InfoBar.Message = "配置文件损坏，请修复！";
            //To Do: 添加进入重设密码界面
            ShowResetMKDialog();
        }
        else
        {
            //报错：未知错误
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Background = new SolidColorBrush(Color.FromArgb(120, 255, 0, 0));
            Login_InfoBar.Message = "未知错误！";
        }
    }

    private async void ShowSetMKDialog()//密码设置弹窗
    {
        SetMKDialog dialog = new()
        {
            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            XamlRoot = this.XamlRoot,
            Style = App.Current.Resources["DefaultContentDialogStyle"] as Style
        };
        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Background = new SolidColorBrush(Color.FromArgb(100, 98, 255, 223));//设置提示为绿色
            Login_InfoBar.Message = "成功设置主密码！";
        }
    }

    private async void ShowResetMKDialog()//重设密码弹窗
    {
        SetMKDialog dialog = new()
        {
            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            XamlRoot = this.XamlRoot,
            Style = App.Current.Resources["DefaultContentDialogStyle"] as Style,
            Title = "请重新设置密码"
        };
        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Background = new SolidColorBrush(Color.FromArgb(100, 98, 255, 223));//设置提示为绿色
            Login_InfoBar.Message = "成功重设主密码";
        }
    }

    private void Login_PssswordBox_PasswordChanging(PasswordBox sender, PasswordBoxPasswordChangingEventArgs args)
    {
        Login_InfoBar.IsOpen = false;
    }
}
