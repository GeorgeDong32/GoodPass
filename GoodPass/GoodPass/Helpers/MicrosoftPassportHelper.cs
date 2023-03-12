using GoodPass.Models;
using Windows.Security.Credentials;
using Windows.Security.Credentials.UI;

namespace GoodPass.Helpers;
public static class MicrosoftPassportHelper
{
    /// <summary>
    /// Checks to see if Passport is ready to be used.
    /// 
    /// Passport has dependencies on:
    ///     1. Having a connected Microsoft Account
    ///     2. Having a Windows PIN set up for that _account on the local machine
    /// </summary>
    public static async Task<bool> MicrosoftPassportAvailableCheckAsync()
    {
        var keyCredentialAvailable = await KeyCredentialManager.IsSupportedAsync();
        if (keyCredentialAvailable == false)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Creates a Passport key on the machine using the _account id passed.
    /// </summary>
    /// <param name="accountId">The _account id associated with the _account that we are enrolling into Passport</param>
    /// <returns>Boolean representing if creating the Passport key succeeded</returns>
    public static async Task<bool> CreatePassportKeyAsync(string accountId)
    {
        var keyCreationResult = await KeyCredentialManager.RequestCreateAsync(accountId, KeyCredentialCreationOption.ReplaceExisting);

        switch (keyCreationResult.Status)
        {
            case KeyCredentialStatus.Success:
                return true;
            case KeyCredentialStatus.UserCanceled:
                break;
            case KeyCredentialStatus.NotFound:
                break;
            default:
                break;
        }
        return false;
    }

    public static async Task<bool> RemovePassportKeyAsync(string accountId)
    {
        await KeyCredentialManager.DeleteAsync(accountId);

        return true;
    }

    public static async Task<PassportSignInResult> PassportSignInAsync()
    {
        //TODO:多语言
        var result = await UserConsentVerifier.RequestVerificationAsync("登录到GoodPass");
        return result switch
        {
            UserConsentVerificationResult.Verified => PassportSignInResult.Verified,
            UserConsentVerificationResult.DeviceBusy => PassportSignInResult.Busy,
            UserConsentVerificationResult.DeviceNotPresent => PassportSignInResult.NotUseable,
            UserConsentVerificationResult.DisabledByPolicy => PassportSignInResult.Disabled,
            UserConsentVerificationResult.RetriesExhausted => PassportSignInResult.Failed,
            UserConsentVerificationResult.Canceled => PassportSignInResult.Cancel,
            UserConsentVerificationResult.NotConfiguredForUser => PassportSignInResult.NotUseable,
            _ => PassportSignInResult.NotUseable,
        };
    }
}