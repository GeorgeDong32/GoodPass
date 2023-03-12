using GoodPass.Helpers;

namespace GoodPass.Services;

public static class MicrosoftPassportService
{
    /// <summary>
    /// 设置MicrosoftPassport，并将主密码置于保险箱中用于以后解锁
    /// </summary>
    /// <param name="username">用户名(base64的IV)</param>
    /// <param name="masterkey">主密码</param>
    /// <returns></returns>
    public static async Task<bool> SetMicrosoftPassportAsync(string username, string masterkey)
    {
        /*if (!MicrosoftPassportHelper.MicrosoftPassportAvailableCheckAsync().Result)
        {
            return false;
        }*/
        var createResult = await Helpers.MicrosoftPassportHelper.CreatePassportKeyAsync("GoodPass");
        if (!createResult)
        {
            return false;
        }
        var vault = new Windows.Security.Credentials.PasswordVault();
        vault.Add(new Windows.Security.Credentials.PasswordCredential("GoodPass", username, masterkey));
        _ = await SecurityStatusHelper.SetMSPassportStatusAsync(true);
        return true;
    }

    /// <summary>
    /// 使用MSP登录并获取主密码
    /// </summary>
    /// <param name="username"></param>
    /// <returns>主密码</returns>
    /// <exception cref="Exception"></exception>
    public static async Task<string> SignInMicrosoftPassportAsync(string username)
    {
        /*if (!MicrosoftPassportHelper.MicrosoftPassportAvailableCheckAsync().Result)
        {
            throw new Exception("SignInMicrosoftPassportAsync: MicrosoftPassport is not available!");
        }*/
        var signinResult = await MicrosoftPassportHelper.PassportSignInAsync();
        switch (signinResult)
        {
            case Models.PassportSignInResult.Verified:
                var vault = new Windows.Security.Credentials.PasswordVault();
                var credential = vault.Retrieve("GoodPass", username);
                return credential.Password;
            case Models.PassportSignInResult.Busy:
                return "Deivce is busy now";
            case Models.PassportSignInResult.Failed:
                return "Verification is failed";
            case Models.PassportSignInResult.Disabled:
                return "MSP services is disabled";
            case Models.PassportSignInResult.NotUseable:
                return "MSP services is not usable";
            case Models.PassportSignInResult.Cancel:
                return "Verification is canceled by user";
            default:
                throw new Exception("SignInMicrosoftPassportAsync: Unkonwn error");
        }
    }

    public static async Task<bool> RemoveMicrosoftPassportAsync(string username, string masterkey)
    {
        /*if (!MicrosoftPassportHelper.MicrosoftPassportAvailableCheckAsync().Result)
        {
            return false;
        }*/
        var removeResult = await Helpers.MicrosoftPassportHelper.RemovePassportKeyAsync("GoodPass");
        if (!removeResult)
        {
            return false;
        }
        var vault = new Windows.Security.Credentials.PasswordVault();
        vault.Remove(new Windows.Security.Credentials.PasswordCredential("GoodPass", username, masterkey));
        _ = await SecurityStatusHelper.SetMSPassportStatusAsync(false);
        return true;
    }
}