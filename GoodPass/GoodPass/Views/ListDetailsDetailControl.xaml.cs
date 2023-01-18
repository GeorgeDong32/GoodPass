using GoodPass.Helpers;
using GoodPass.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;

namespace GoodPass.Views;

public sealed partial class ListDetailsDetailControl : UserControl
{
    public GPData? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as GPData;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }
    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(GPData), typeof(ListDetailsDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public ListDetailsDetailControl()
    {
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ListDetailsDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }

    private void ListDetailsDetailControl_PasswordCopyButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var password = ListDetailsDetailControl_PasswordBox.Password;
        var passwordDatapackage = new DataPackage();
        passwordDatapackage.SetText(password);
        Clipboard.SetContent(passwordDatapackage);
        CopiedTipforPasswordCopyButton.IsOpen = true;
    }

    private void ListDetailsDetailControl_AcconutNameCopyButton_Click(object sender, RoutedEventArgs e)
    {
        var accountName = ListDetailsDetailControl_AccountNameText.Text;
        var dataPackage = new DataPackage();
        dataPackage.SetText(accountName);
        Clipboard.SetContent(dataPackage);
        CopiedTipforAcconutNameCopyButton.IsOpen = true;
    }

    private void PasswordRevealButton_Click(object sender, RoutedEventArgs e)
    {
        if (PasswordRevealButton.IsChecked == true)
            ListDetailsDetailControl_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
        else
            ListDetailsDetailControl_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
    }

    private void ListDetailsDetailControl_EditButton_Click(object sender, RoutedEventArgs e)
    {
        //Todo：添加编辑模式代码
    }

    private async void ListDetailsDetailControl_DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        //弹窗提示用户确认
        var dialog = new GPDialog2();
        dialog.XamlRoot = this.XamlRoot;
        dialog.Style = App.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = "删除确认";
        dialog.Content = "您确定要删除这个数据吗？该操作不可撤销！";
        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            var tarPlatform = ListDetailsMenuItem.PlatformName;
            var tarAccountName = ListDetailsMenuItem.AccountName;
            try
            {
                App.ListDetailsVM.DeleteDataItem(App.DataManager.GetData(tarPlatform, tarAccountName));
                var delResult = App.DataManager.DeleteData(tarPlatform, tarAccountName);
                if (delResult == false)
                    throw new GPObjectNotFoundException("Data Not Found!");
            }
            catch (System.ArgumentOutOfRangeException)
            {
                var warningDialog = new GPDialog2();
                warningDialog.XamlRoot = XamlRoot;
                warningDialog.Style = App.Current.Resources["DefaultContentDialogStyle"] as Style;
                warningDialog.Title = "出错了！";
                warningDialog.Content = "您试图删除一个不存在的对象";
                var _ = await warningDialog.ShowAsync();
            }
            catch (GPObjectNotFoundException)
            {
                var warningDialog = new GPDialog2();
                warningDialog.XamlRoot = XamlRoot;
                warningDialog.Style = App.Current.Resources["DefaultContentDialogStyle"] as Style;
                warningDialog.Title = "出错了！";
                warningDialog.Content = "您试图删除一个不存在的对象";
                var _ = await warningDialog.ShowAsync();
            }
        }
    }

    private async void ListDetailsDetailControl_PlatformUrlHyperLink_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ListDetailsDetailControl_PlatformUrlHyperLink.NavigateUri = new Uri(ListDetailsMenuItem.PlatformUrl);
        }
        catch (ArgumentNullException)
        {
            ListDetailsDetailControl_PlatformUrlHyperLink.NavigateUri = null;
            var warningDialog = new GPDialog2();
            warningDialog.XamlRoot = XamlRoot;
            warningDialog.Style = App.Current.Resources["DefaultContentDialogStyle"] as Style;
            warningDialog.Title = "出错了！";
            warningDialog.Content = "链接为空，无法访问！";
            var _ = await warningDialog.ShowAsync();
        }
        catch (UriFormatException)
        {
            ListDetailsDetailControl_PlatformUrlHyperLink.NavigateUri = null;
            var warningDialog = new GPDialog2();
            warningDialog.XamlRoot = XamlRoot;
            warningDialog.Style = App.Current.Resources["DefaultContentDialogStyle"] as Style;
            warningDialog.Title = "出错了！";
            warningDialog.Content = "链接为空，无法访问！";
            var _ = await warningDialog.ShowAsync();
        }
    }
}
