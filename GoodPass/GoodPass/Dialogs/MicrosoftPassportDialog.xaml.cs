using GoodPass.Services;
using Microsoft.UI.Xaml.Controls;

namespace GoodPass.Dialogs;

/// <summary>
/// 启用或禁用MSP时的验证弹窗
/// </summary>
public sealed partial class MicrosoftPassportDialog : ContentDialog
{
    public string MasterKey;

    public MicrosoftPassportDialog()
    {
        MasterKey = string.Empty;
        this.InitializeComponent();
        IsPrimaryButtonEnabled = false;
        MPD_PasswordCheckText.Text = App.UIStrings.MPD_PasswordCheckFailed;
    }

    private void MPD_PasswordBox_PasswordChanged(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var checkResult = MasterKeyService.CheckMasterKey_NP(MPD_PasswordBox.Password);
        if (checkResult == "pass")
        {
            MasterKey = MPD_PasswordBox.Password;
            MPD_PasswordCheckIcon.Glyph = "\xE73E";
            MPD_PasswordCheckText.Text = App.UIStrings.MPD_PasswordCheckSuccess;
            IsPrimaryButtonEnabled = true;
        }
        else
        {
            MPD_PasswordCheckIcon.Glyph = "\xE711";
            MPD_PasswordCheckText.Text = App.UIStrings.MPD_PasswordCheckFailed;
            IsPrimaryButtonEnabled = false;
        }
    }
}
