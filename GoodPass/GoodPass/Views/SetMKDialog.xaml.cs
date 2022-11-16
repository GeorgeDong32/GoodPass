using GoodPass.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GoodPass.Views;

public sealed partial class SetMKDialog : ContentDialog
{
    public bool _isComplete
    {
        get; set;
    }
    private bool _isSecure
    {
        get; set;
    }

    public SetMKDialog()
    {
        _isComplete = false; _isSecure = false;
        this.InitializeComponent();
        IsPrimaryButtonEnabled = false;
    }

    /*To Do: 设置密码相关逻辑代码*/
    private void RevealModeCheckbox_Changed1(object sender, RoutedEventArgs e)
    {
        if (SetMKDialog_PB1CheckBox.IsChecked == true)
        {
            SetMKDialog_PssswordBox1.PasswordRevealMode = PasswordRevealMode.Visible;
        }
        else
        {
            SetMKDialog_PssswordBox1.PasswordRevealMode = PasswordRevealMode.Hidden;
        }
    }

    private void RevealModeCheckbox_Changed2(object sender, RoutedEventArgs e)
    {
        if (SetMKDialog_PB2CheckBox.IsChecked == true)
        {
            SetMKDialog_PssswordBox2.PasswordRevealMode = PasswordRevealMode.Visible;
        }
        else
        {
            SetMKDialog_PssswordBox2.PasswordRevealMode = PasswordRevealMode.Hidden;
        }
    }

    private void SetMKDialog_PssswordBox2_PasswordChanged(object sender, RoutedEventArgs e)
    {
        var MK1 = SetMKDialog_PssswordBox1.Password;
        if (SetMKDialog_PssswordBox2.Password != MK1)
        {
            SetMKDialog_PB2Status.Text = "两次密码不一致";
            IsPrimaryButtonEnabled = false;
        }
        else
        {
            SetMKDialog_PB2Status.Text = string.Empty;
            if (_isSecure)
                IsPrimaryButtonEnabled = true;
            else
                IsPrimaryButtonEnabled = false;
        }
    }

    private void SetMKDialog_PssswordBox1_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (SetMKDialog_PssswordBox1.Password.Length < 8)
        {
            SetMKDialog_PB1Status.Text = "主密码过短！";
            _isSecure = false;
            IsPrimaryButtonEnabled = false;
        }
        else if (SetMKDialog_PssswordBox1.Password.Length >= 8 && SetMKDialog_PssswordBox1.Password.Length <= 40)
        {
            SetMKDialog_PB1Status.Text = string.Empty;
            _isSecure = true;
        }
        else
        {
            SetMKDialog_PB1Status.Text = "主密码过长！";
            _isSecure = false;
            IsPrimaryButtonEnabled = false;
        }
        var MK1 = SetMKDialog_PssswordBox1.Password;
        if (SetMKDialog_PssswordBox2.Password != MK1)
        {
            SetMKDialog_PB2Status.Text = "两次密码不一致";
            IsPrimaryButtonEnabled = false;
        }
        else
        {
            SetMKDialog_PB2Status.Text = string.Empty;
            if (_isSecure)
                IsPrimaryButtonEnabled = true;
            else
                IsPrimaryButtonEnabled = false;
        }
    }

    private void SetMKDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        var MKS = new MasterKeyService();
        var MK = SetMKDialog_PssswordBox1.Password;
        MKS.SetLocalMKHash(MK);
    }
}