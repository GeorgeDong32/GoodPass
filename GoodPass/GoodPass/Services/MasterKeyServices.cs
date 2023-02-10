using System.ComponentModel;
using GoodPass.Contracts.Services;
using GoodPass.Helpers;
using GoodPass.Models;
using Microsoft.UI.Composition;
using Windows.Networking.Vpn;
using Windows.Storage;

namespace GoodPass.Services;

/// <summary>
/// Provide services such as verifying the master password and setting the master password.
/// </summary>
public class MasterKeyService : IMaterKeyService
{
    private string localMKHash;
    readonly string userName;

    readonly string appdataPath;

    private readonly string localMKPath;

    public MasterKeyService()
    {
        userName = Environment.UserName;
        appdataPath = $"C:\\Users\\{userName}\\AppData\\Local";
        localMKPath = Path.Combine(appdataPath, "GoodPass", "MKconfig.txt");
        localMKHash = "";
    }

    /// <summary>
    /// 设置本地哈希校验值
    /// </summary>
    /// <param name="MasterKey">主密码</param>
    public bool SetLocalMKHash(string MasterKey)
    {
        var GoodPassFolderPath = Path.Combine(appdataPath, "GoodPass");
        var MKconfigPath = Path.Combine(GoodPassFolderPath, "MKconfig.txt");
        if (!System.IO.Directory.Exists(GoodPassFolderPath))
        {
            System.IO.Directory.CreateDirectory(GoodPassFolderPath);
            if (System.IO.Directory.Exists(GoodPassFolderPath))
            {
                System.IO.File.Create(MKconfigPath).Close();
                if (System.IO.File.Exists(MKconfigPath))
                {
                    System.IO.File.WriteAllText(MKconfigPath, GoodPassSHAServices.getGPHES(MasterKey));
                    return true;
                }
                else
                {
                    return false;
                    throw new Exception("Failed to create config file!");
                }
            }
            else
            {
                return false;
                throw new Exception("Failed to create data folder!");
            }
        }
        else if (System.IO.Directory.Exists(GoodPassFolderPath))
        {
            if (!System.IO.File.Exists(MKconfigPath))
            {
                System.IO.File.Create(MKconfigPath).Close();
                if (System.IO.File.Exists(MKconfigPath))
                {
                    System.IO.File.WriteAllText(MKconfigPath, GoodPassSHAServices.getGPHES(MasterKey));
                    return true;
                }
                else
                {
                    return false;
                    throw new Exception("Failed to create config file!");
                }
            }
            else
            {
                System.IO.File.WriteAllText(MKconfigPath, GoodPassSHAServices.getGPHES(MasterKey));
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 获取本地主密码哈希校验值
    /// </summary>
    /// <returns>本地哈希校验值</returns>
    public string GetLocalMKHash()
    {
        var LocalMKHash = "";
        try
        {
            LocalMKHash = File.ReadAllText(localMKPath);
        }
        catch (System.IO.DirectoryNotFoundException)
        {
            localMKHash = "Not found";
            LocalMKHash = "Not found";
        }
        catch (System.IO.FileNotFoundException)
        {
            localMKHash = "Not found";
            LocalMKHash = "Not found";
        }
        finally
        {
            if (LocalMKHash == "")
                localMKHash = "Empty";
            else
                localMKHash = LocalMKHash;
        }
        return localMKHash;
    }

    /// <summary>
    /// (封装的)校验主密码方法
    /// </summary>
    /// <param name="InputKey">输入的主密码</param>
    /// <returns>校验结果</returns>
    public string CheckMasterKey(string InputKey)
    {
        var InputKeyHash = GoodPassSHAServices.getGPHES(InputKey);
        GetLocalMKHash();
        if (InputKeyHash == localMKHash)
        {
            ProcessMKArray(InputKey);
            return "pass";
        }
        else if (localMKHash == "Not found")
            return "error: not found";
        else if (localMKHash == "Empty")
            return "error: data broken";
        else if (InputKeyHash != localMKHash)
            return "npass";
        else return "Unknown Error";
    }

    /// <summary>
    /// 生成App的加密基和主密码基
    /// </summary>
    public void ProcessMKArray(string InputKey)
    {
        App.EncryptBase = new int[40] { 1, 4, 1, 5, 9, 2, 6, 5, 3, 5, 8, 9, 7, 9, 3, 2, 3, 8, 4, 6, 2, 6, 4, 3, 3, 8, 3, 2, 7, 9, 5, 0, 2, 8, 8, 4, 1, 9, 7, 1 };
        App.MKBase = App.EncryptBase;
        var MaxLength = Math.Min(40, InputKey.Length);
        for (var i = 0; i < MaxLength; i++)
        {
            var key = InputKey[i];
            if (key >= 'a' && key <= 'z')
            {
                var temp = key - 'a';
                while (temp >= 10)
                {
                    App.MKBase[i] = temp / 10;
                    i++;
                    temp %= 10;
                }
                App.MKBase[i] = temp;
            }
            else if (key >= 'A' && key <= 'Z')
            {
                var temp = key - 'A';
                while (temp >= 10)
                {
                    App.MKBase[i] = temp / 10;
                    i++;
                    temp %= 10;
                }
                App.MKBase[i] = temp;
            }
            else if (key >= '0' && key <= '9')
            {
                App.MKBase[i] = key - '0';
            }
            else
            {
                App.MKBase[i] = App.EncryptBase[i];
            }
            if (i >= 40)//防止溢出
            {
                break;
            }
        }
    }

    /// <summary>
    /// (异步)获取本地主密码哈希校验值
    /// </summary>
    /// <returns>本地哈希校验值</returns>
    public async Task<string> GetLocalMKHashAsync()/*ToDo：通过RATAsync的异常机制精简方法*/
    {
        Task<string> LocalMKHash = TaskTConverter.StringToTaskString(""); ;
        try
        {
            //使用部分同步方法用以解决异步方法不抛出异常的问题
            var tryreadfile = File.ReadAllText(localMKPath);
            LocalMKHash = File.ReadAllTextAsync(localMKPath);
        }
        catch (System.IO.DirectoryNotFoundException)
        {
            localMKHash = "Not found";
            LocalMKHash = TaskTConverter.StringToTaskString("Not found");
        }
        catch (System.IO.FileNotFoundException)
        {
            localMKHash = "Not found";
            LocalMKHash = TaskTConverter.StringToTaskString("Not found");
        }
        finally
        {
            if (LocalMKHash.Result == String.Empty)
                localMKHash = "Empty";
            else
                localMKHash = LocalMKHash.Result;
        }
        return await LocalMKHash;
    }

    /// <summary>
    /// (封装的异步)校验主密码方法
    /// </summary>
    /// <param name="inputKey">输入的主密码</param>
    /// <returns>校验结果</returns>
    public async Task<string> CheckMasterKeyAsync(string inputKey)
    {
        var InputKeyHash = GoodPassSHAServices.getGPHES(inputKey);
        var LocalMKHash = await GetLocalMKHashAsync();
        if (InputKeyHash == LocalMKHash)
        {
            ProcessMKArray(inputKey);
            return "pass";
        }
        else if (LocalMKHash == "Not found")
            return "error: not found";
        else if (LocalMKHash == String.Empty)
            return "error: data broken";
        else if (InputKeyHash != LocalMKHash)
            return "npass";
        else return "Unknown Error";
    }

    /// <summary>
    /// MSIX打包应用的设置主密码方法
    /// </summary>
    /// <returns>设置密码状态</returns>
    /// <exception cref="GPRuntimeException">未在MSIX环境中运行</exception>
    public async Task<int> SetMasterKeyAsync_MSIX(string inputKey)
    {
        /// Return value table
        /// 0 -- Successfully set localhash
        /// 2 -- Already have localhash
        /// 

        var inputKeyHash = GoodPassSHAServices.getGPHES(inputKey);
        if (RuntimeHelper.IsMSIX)
        {
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("LocalMKHash", out var obj))
            {
                var localHash = (string)obj;
                if (localHash == String.Empty || obj == null)
                {
                    ApplicationData.Current.LocalSettings.Values["LocalMKHash"] = inputKeyHash;
                    await Task.CompletedTask;
                    return 0;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                ApplicationData.Current.LocalSettings.Values["LocalMKHash"] = inputKeyHash;
                await Task.CompletedTask;
                return 0;
            }
        }
        else
        {
            throw new GPRuntimeException("CheckMasterKeyAsync_MSIX: Not Run in MSIX");
        }
    }

    /// <summary>
    /// MSIX打包应用使用的主密码校验方法
    /// </summary>
    /// <exception cref="GPRuntimeException">未在MSIX环境中运行</exception>
    public async Task<string> CheckMasterKeyAsync_MSIX(string inputKey)
    {
        var inputKeyHash = GoodPassSHAServices.getGPHES(inputKey);
        if (RuntimeHelper.IsMSIX)
        {
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("LocalMKHash", out var obj))
            {
                var localHash = (string)obj;
                await Task.CompletedTask;
                if (localHash == String.Empty)
                {
                    return "error: data broken";
                }
                else if (inputKeyHash == localHash)
                {
                    ProcessMKArray(inputKey);
                    return "pass";
                }
                else if (inputKeyHash != localHash)
                {
                    return "npass";
                }
                else
                {
                    return "Unknown Error";
                }
            }
            else
            {
                return "error: not found";
            }
        }
        else
        {
            throw new GPRuntimeException("CheckMasterKeyAsync_MSIX: Not Run in MSIX");
        }
    }
}
