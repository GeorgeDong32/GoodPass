using Windows.Storage;

namespace GoodPass.Helpers;

public static class SecurityStatusHelper
{
    public static async Task<bool> GetMSPassportStatusAsync()
    {
        if (RuntimeHelper.IsMSIX)
        {
            bool MSPS;
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("MSPassport", out var obj))
            {
                MSPS = (string)obj switch
                {
                    "True" => true,
                    "False" => false,
                    _ => false,
                };
            }
            else
            {
                MSPS = false;
            }
            await Task.CompletedTask;
            return MSPS;
        }
        else
        {
            throw new GPRuntimeException("GetMSPassportStatus: Not in MSIX");
        }
    }

    public static async Task<bool> SetMSPassportStatusAsync(bool value)
    {
        if (RuntimeHelper.IsMSIX)
        {
            ApplicationData.Current.LocalSettings.Values["MSPassport"] = value.ToString();
            await Task.CompletedTask;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static async Task<bool> GetDataInsetStatusAsync()
    {
        if (RuntimeHelper.IsMSIX)
        {
            bool DIS;
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("DataInsert", out var obj))
            {
                DIS = (string)obj switch
                {
                    "True" => true,
                    "False" => false,
                    _ => false,
                };
            }
            else
            {
                DIS = false;
            }
            await Task.CompletedTask;
            return DIS;
        }
        else
        {
            throw new GPRuntimeException("GetDataInsetStatusAsync: Not in MSIX");
        }
    }

    public static async Task<bool> SetDataInsetStatusAsync(bool value)
    {
        if (RuntimeHelper.IsMSIX)
        {
            ApplicationData.Current.LocalSettings.Values["DataInsert"] = value.ToString();
            await Task.CompletedTask;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static async Task<bool> GetAESStatusAsync()
    {
        if (RuntimeHelper.IsMSIX)
        {
            bool AESS;
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("AESStatus", out var obj))
            {
                AESS = (string)obj switch
                {
                    "True" => true,
                    "False" => false,
                    _ => false,
                };
            }
            else
            {
                AESS = false;
            }
            await Task.CompletedTask;
            return AESS;
        }
        else
        {
            throw new GPRuntimeException("GetAESStatusAsync: Not in MSIX");
        }
    }

    public static async Task<bool> SetAESStatusAsync(bool value)
    {
        if (RuntimeHelper.IsMSIX)
        {
            ApplicationData.Current.LocalSettings.Values["AESStatus"] = value.ToString();
            await Task.CompletedTask;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static async Task<bool> SetVaultUsername(string username)
    {
        if (RuntimeHelper.IsMSIX)
        {
            ApplicationData.Current.LocalSettings.Values["VaultUsername"] = username;
            await Task.CompletedTask;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static async Task<string> GetVaultUsername()
    {
        if (RuntimeHelper.IsMSIX)
        {
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("VaultUsername", out var obj))
            {
                await Task.CompletedTask;
                return (string)obj;
            }
            else
            {
                throw new Exception("GetVaultUsername: No Vault Username");
            }
        }
        else
        {
            throw new GPRuntimeException("GetVaultUsername: Not in MSIX");
        }
    }
}