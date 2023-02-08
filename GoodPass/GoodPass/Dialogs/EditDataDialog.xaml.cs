using GoodPass.Models;
using GoodPass.Services;
using Microsoft.UI.Xaml.Controls;

namespace GoodPass.Dialogs;

public sealed partial class EditDataDialog : ContentDialog
{
    public EditDataResult Result
    {
        get; set;
    }

    //旧数据
    private readonly string oldAccountName;
    private readonly string oldPlatformName;
    private readonly string oldPassword;
    private readonly string? oldPlatformUrl;

    //新数据
    public string newAccountName;
    public string newPlatformName;
    public string newPassword;
    public string? newPlatformUrl;
    public DateTime newDateTime;

    /// <summary>
    /// 编辑对话窗构造函数
    /// </summary>
    public EditDataDialog(string accountName, string platformName, string platformUrl, string password)
    {
        this.InitializeComponent();
        IsPrimaryButtonEnabled = true;
        Result = EditDataResult.Nochange;
        EditDataDialog_PlatformBox.Text = platformName;
        EditDataDialog_AccountBox.Text = accountName;
        EditDataDialog_PasswordBox.Password = password;
        EditDataDialog_PlatformUrlBox.Text = platformUrl;
        oldAccountName = accountName;
        oldPlatformName = platformName;
        oldPassword = password;
        oldPlatformUrl = platformUrl;
        newAccountName = accountName;
        newPlatformName = platformName;
        newPassword = password;
        newPlatformUrl = platformUrl;
        newDateTime = DateTime.Now;
        if (platformUrl != String.Empty)
        {
            EditDataDialog_UrlCheckIcon.Glyph = "\xE73E";
            EditDataDialog_UrlCheckText.Text = "平台Url合法";
        }
    }

    /// <summary>
    /// 生成无特殊字符的随机密码
    /// </summary>
    private void EditDataDialog_PasswordMode_RandomNoSpec_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        EditDataDialog_PasswordModeText.Text = EditDataDialog_PasswordMode_RandomNoSpec.Text;
        if (EditDataDialog_PasswordLengthBox.Text == String.Empty)
        {
            EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
        }
        else
        {
            var genLength = 0;
            var LengthCheck = true;
            try
            {
                genLength = Convert.ToInt32(EditDataDialog_PasswordLengthBox.Text);
            }
            catch (FormatException)
            {
                EditDataDialog_PasswordLengthTeachtip.Title = "输入长度非数字！";
                EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
                LengthCheck = false;
            }
            catch (OverflowException)
            {
                EditDataDialog_PasswordLengthTeachtip.Title = "输入长度超限！";
                EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
                LengthCheck = false;
            }
            if (LengthCheck)
            {
                if (genLength > 0 && genLength <= 24)
                {
                    EditDataDialog_PasswordBox.Password = GoodPassPWGService.RandomPasswordNormal(genLength);
                }
                else
                {
                    EditDataDialog_PasswordLengthTeachtip.Title = "输入长度超限";
                    EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
                }
            }
        }
    }

    /// <summary>
    /// 生成含特殊字符的随机密码
    /// </summary>
    private void EditDataDialog_PasswordMode_RandomSpec_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        EditDataDialog_PasswordModeText.Text = EditDataDialog_PasswordMode_RandomSpec.Text;
        if (EditDataDialog_PasswordLengthBox.Text == String.Empty)
        {
            EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
        }
        else
        {
            var genLength = 0;
            var LengthCheck = true;
            try
            {
                genLength = Convert.ToInt32(EditDataDialog_PasswordLengthBox.Text);
            }
            catch (FormatException)
            {
                EditDataDialog_PasswordLengthTeachtip.Title = "输入长度非数字！";
                EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
                LengthCheck = false;
            }
            catch (OverflowException)
            {
                EditDataDialog_PasswordLengthTeachtip.Title = "输入长度超限！";
                EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
                LengthCheck = false;
            }
            if (LengthCheck)
            {
                if (genLength > 0 && genLength <= 24)
                {
                    EditDataDialog_PasswordBox.Password = GoodPassPWGService.RandomPasswordSpec(genLength);
                }
                else
                {
                    EditDataDialog_PasswordLengthTeachtip.Title = "输入长度超限";
                    EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
                }
            }
        }
    }

    /// <summary>
    /// 生成GoodPass样式的密码
    /// </summary>
    private void EditDataDialog_PasswordMode_GPStyle_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        EditDataDialog_PasswordModeText.Text = EditDataDialog_PasswordMode_GPStyle.Text;
        if (EditDataDialog_AccountBox.Text == String.Empty)
        {
            if (EditDataDialog_PlatformBox.Text == String.Empty)
            {
                EditDataDialog_PasswordModeTeachtip.Title = "账户名和平台名不能为空！";
                EditDataDialog_PasswordModeTeachtip.IsOpen = true;
            }
            else if (EditDataDialog_PlatformBox.Text != String.Empty)
            {
                EditDataDialog_PasswordModeTeachtip.Title = "账户名不能为空！";
                EditDataDialog_PasswordModeTeachtip.IsOpen = true;
            }
        }
        else if (EditDataDialog_PlatformBox.Text == String.Empty)
        {
            EditDataDialog_PasswordModeTeachtip.Title = "平台名不能为空！";
            EditDataDialog_PasswordModeTeachtip.IsOpen = true;
        }
        else
        {
            EditDataDialog_PasswordBox.Password = GoodPassPWGService.GPstylePassword(EditDataDialog_PlatformBox.Text, EditDataDialog_AccountBox.Text);
        }
    }

    private void Add_PasswordRevealButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (Add_PasswordRevealButton.IsChecked == true)
        {
            EditDataDialog_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
        }
        else
        {
            EditDataDialog_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
        }
    }

    private void EditDataDialog_PlatformBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (EditDataDialog_PlatformBox.Text != String.Empty)
        {
            EditDataDialog_PlatformCheckIcon.Glyph = "\xE73E";
            EditDataDialog_PlatformCheckText.Text = "平台名合法";
            if (EditDataCheck())
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }
        else
        {
            EditDataDialog_PlatformCheckIcon.Glyph = "\xE711";
            EditDataDialog_PlatformCheckText.Text = "平台名不能为空";
            IsPrimaryButtonEnabled = false;
        }
    }

    private void EditDataDialog_AccountBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (EditDataDialog_AccountBox.Text != String.Empty)
        {
            EditDataDialog_AccountCheckIcon.Glyph = "\xE73E";
            EditDataDialog_AccountCheckText.Text = "平台名合法";
            if (EditDataCheck())
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }
        else
        {
            EditDataDialog_AccountCheckIcon.Glyph = "\xE711";
            EditDataDialog_AccountCheckText.Text = "平台名不能为空";
            IsPrimaryButtonEnabled = false;
        }
    }

    private void EditDataDialog_PasswordBox_PasswordChanged(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (EditDataDialog_PasswordBox.Password != String.Empty)
        {
            if (EditDataCheck())
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }
        else
        {
            IsPrimaryButtonEnabled = false;
        }
    }

    private void EditDataDialog_PlatformUrlBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (EditDataDialog_PlatformUrlBox.Text != String.Empty)
        {
            var checkStatus = true;
            try
            {
                var url = new Uri(EditDataDialog_PlatformUrlBox.Text);
            }
            catch (UriFormatException)
            {
                checkStatus = false;
            }
            if (checkStatus)
            {
                EditDataDialog_UrlCheckIcon.Glyph = "\xE73E";
                EditDataDialog_UrlCheckText.Text = "平台Url合法";
                if (EditDataCheck())
                {
                    IsPrimaryButtonEnabled = true;
                }
                else
                {
                    IsPrimaryButtonEnabled = false;
                }
            }
            else
            {
                EditDataDialog_UrlCheckIcon.Glyph = "\xE711";
                EditDataDialog_UrlCheckText.Text = "请使用http开头的完整Url格式";
                IsPrimaryButtonEnabled = false;
            }
        }
        else
        {
            EditDataDialog_UrlCheckIcon.Glyph = "\xE946";
            EditDataDialog_UrlCheckText.Text = "Url为空，可选择添加链接";
            if (EditDataCheck())
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }
    }

    /// <summary>
    /// 编辑确定后的处理
    /// </summary>
    private void EditDataDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        if (EditDataDialog_PlatformBox.Text == oldPlatformName && EditDataDialog_AccountBox.Text == oldAccountName && EditDataDialog_PasswordBox.Password == oldPassword && EditDataDialog_PlatformUrlBox.Text == oldPlatformUrl)
        {
            this.Result = EditDataResult.Nochange;
        }
        else
        {
            var accountNameChanged = false;
            if (EditDataDialog_PlatformUrlBox.Text != oldPlatformUrl)
            {
                newPlatformUrl = EditDataDialog_PlatformUrlBox.Text;
                var check = App.DataManager.ChangeUrl(oldPlatformName, oldAccountName, EditDataDialog_PlatformUrlBox.Text);
                if (check)
                {
                    this.Result = EditDataResult.Success;
                }
                else
                {
                    this.Result = EditDataResult.Failure;
                }
            }
            if (EditDataDialog_PasswordBox.Password != oldPassword)
            {
                newPassword = EditDataDialog_PasswordBox.Password;
                var check = App.DataManager.ChangePassword(oldPlatformName, oldAccountName, EditDataDialog_PasswordBox.Password);
                switch (check)
                {
                    case "Success":
                        this.Result = EditDataResult.Success;
                        break;
                    case "SamePassword":
                        this.Result = EditDataResult.Failure;
                        break;
                    case "Empty":
                        this.Result = EditDataResult.Failure;
                        break;
                    case "Unknown Error":
                        this.Result = EditDataResult.UnknowError;
                        break;
                }
            }
            if (EditDataDialog_AccountBox.Text != oldAccountName)
            {
                newAccountName = EditDataDialog_AccountBox.Text;
                var check = App.DataManager.ChangeAccountName(oldPlatformName, oldAccountName, EditDataDialog_AccountBox.Text);
                if (check)
                {
                    if (check)
                    {
                        this.Result = EditDataResult.Success;
                        accountNameChanged = true;
                    }
                    else
                        this.Result = EditDataResult.Failure;
                }
                else
                {
                    this.Result = EditDataResult.Failure;
                }
            }
            if (EditDataDialog_PlatformBox.Text != oldPlatformName)
            {
                newPlatformName = EditDataDialog_PlatformBox.Text;
                if (accountNameChanged)
                {
                    var newAccountName = EditDataDialog_AccountBox.Text;
                    var check = App.DataManager.ChangePlatformName(oldPlatformName, newAccountName, EditDataDialog_PlatformBox.Text);
                    if (check)
                    {
                        this.Result = EditDataResult.Success;
                    }
                    else
                    {
                        this.Result = EditDataResult.Failure;
                    }
                }
                else
                {
                    var check = App.DataManager.ChangePlatformName(oldPlatformName, oldAccountName, EditDataDialog_PlatformBox.Text);
                    if (check)
                    {
                        this.Result = EditDataResult.Success;
                    }
                    else
                    {
                        this.Result = EditDataResult.Failure;
                    }
                }
            }
        }
        newDateTime = App.DataManager.GetData(newPlatformName, newAccountName).LatestUpdateTime;
        App.ListDetailsVM.OnNavigatedTo(null);
    }

    /// <summary>
    /// 检查编辑弹窗内的数据是否合法
    /// </summary>
    /// <returns>编辑结果是否合法</returns>
    private bool EditDataCheck()
    {
        if (EditDataDialog_PlatformBox.Text != String.Empty && EditDataDialog_AccountBox.Text != String.Empty && EditDataDialog_PasswordBox.Password != String.Empty)
        {
            return EditDataDialog_UrlCheckText.Text == "平台Url合法" || EditDataDialog_UrlCheckText.Text == "Url为空，可选择添加链接";
        }
        else
        {
            return false;
        }
    }
}
