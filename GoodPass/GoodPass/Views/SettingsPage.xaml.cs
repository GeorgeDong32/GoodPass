using System.Security.Cryptography;
using GoodPass.Dialogs;
using GoodPass.Helpers;
using GoodPass.Services;
using GoodPass.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace GoodPass.Views;

public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
        if (App.App_IsLock())
        {
            MicrosoftPassportButton.IsEnabled = false;
            DataInsertButton.IsEnabled = false;
            AESButton.IsEnabled = false;
        }
        switch (SecurityStatusHelper.GetMSPassportStatusAsync().Result)
        {
            case true:
                MicrosoftPassportButton.IsChecked = true;
                MicrosoftPassportSituationIcon.Glyph = "\xE73E";
                MicrosoftPassportSituationText.Text = App.UIStrings.MicrosoftPassportSituatoinText1;
                break;
            case false:
                MicrosoftPassportButton.IsChecked = false;
                MicrosoftPassportSituationIcon.Glyph = "\xE711";
                MicrosoftPassportSituationText.Text = App.UIStrings.MicrosoftPassportSituatoinText2;
                break;
        }
        switch (SecurityStatusHelper.GetDataInsetStatusAsync().Result)
        {
            case true:
                DataInsertButton.IsChecked = true;
                DataInsertSituationIcon.Glyph = "\xE73E";
                DataInsertSituationText.Text = App.UIStrings.DataInsertSituationText1;
                break;
            case false:
                DataInsertButton.IsChecked = false;
                DataInsertSituationIcon.Glyph = "\xE711";
                DataInsertSituationText.Text = App.UIStrings.DataInsertSituationText2;
                break;
        }
        switch (SecurityStatusHelper.GetAESStatusAsync().Result)
        {
            case true:
                AESButton.IsChecked = true;
                AESSituationIcon.Glyph = "\xE73E";
                AESSituationText.Text = App.UIStrings.AESSituationText1;
                break;
            case false:
                AESButton.IsChecked = false;
                AESSituationIcon.Glyph = "\xE711";
                AESSituationText.Text = App.UIStrings.AESSituationText2;
                break;
        }
    }

    private async void MicrosoftPassportButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton tb)
        {
            switch (tb.IsChecked)
            {
                case true:
                    var dialog = new MicrosoftPassportDialog()
                    {
                        XamlRoot = this.XamlRoot,
                        Style = App.Current.Resources["DefaultContentDialogStyle"] as Style,
                        Title = "启用Microsoft Passport"
                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        var masterKey = dialog.MasterKey;
                        var username = Convert.ToBase64String(Aes.Create().IV);
                        var mpResult = await MicrosoftPassportService.SetMicrosoftPassportAsync(username, masterKey);
                        if (mpResult)
                        {
                            _ = SecurityStatusHelper.SetVaultUsername(username);
                        }
                    }
                    else
                    {
                        tb.IsChecked = false;
                        return;
                    }
                    MicrosoftPassportSituationIcon.Glyph = "\xE73E";
                    MicrosoftPassportSituationText.Text = App.UIStrings.MicrosoftPassportSituatoinText1;
                    _ = await SecurityStatusHelper.SetMSPassportStatusAsync(true);
                    break;
                case false:
                    var dialog1 = new MicrosoftPassportDialog()
                    {
                        XamlRoot = this.XamlRoot,
                        Style = App.Current.Resources["DefaultContentDialogStyle"] as Style,
                        Title = "禁用Microsoft Passport"
                    };
                    var result1 = await dialog1.ShowAsync();
                    if (result1 == ContentDialogResult.Primary)
                    {
                        var masterKey = dialog1.MasterKey;
                        var username = await SecurityStatusHelper.GetVaultUsername();
                        var mpResult = await MicrosoftPassportService.RemoveMicrosoftPassportAsync(username, masterKey);
                    }
                    else
                    {
                        tb.IsChecked = true;
                        return;
                    }
                    MicrosoftPassportSituationIcon.Glyph = "\xE711";
                    MicrosoftPassportSituationText.Text = App.UIStrings.MicrosoftPassportSituatoinText2;
                    _ = await SecurityStatusHelper.SetMSPassportStatusAsync(false);
                    break;
            }
        }
    }

    private void DataInsertButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton tb)
        {
            //TODO: 在完成功能后删除
            tb.ContextFlyout.ShowAt(tb);
            tb.IsChecked = false;
            return;
            //TODO: 启用/关闭数据内嵌功能
        }
    }

    private async void AESButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton tb)
        {
            switch (tb.IsChecked)
            {
                case true:
                    AESSituationIcon.Glyph = "\xE73E";
                    AESSituationText.Text = App.UIStrings.AESSituationText1;
                    _ = await SecurityStatusHelper.SetAESStatusAsync(true);
                    App.DataManager.EncryptAllDatas();
                    break;
                case false:
                    AESSituationIcon.Glyph = "\xE711";
                    AESSituationText.Text = App.UIStrings.AESSituationText2;
                    _ = await SecurityStatusHelper.SetAESStatusAsync(false);
                    App.DataManager.EncryptAllDatas();
                    break;
            }
        }
    }
}
