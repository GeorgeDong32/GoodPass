using GoodPass.Contracts.Services;

namespace GoodPass.Services;

public class MasterKeyService : IMaterKeyService
{
    private string _LocalMKHash;
    readonly string userName;

    readonly string _appdataPath;

    private readonly string _LocalMKPath;

    private readonly GoodPassSHAServices GPHESService = new();

    private readonly Helpers.TaskTConverter taskTConverter = new();

    public MasterKeyService()
    {
        userName = Environment.UserName;
        _appdataPath = $"C:\\Users\\{userName}\\AppData\\Local";
        _LocalMKPath = Path.Combine(_appdataPath, "GoodPass", "MKconfig.txt");
        _LocalMKHash = "";
    }

    public bool SetLocalMKHash(string MasterKey)
    {
        var GoodPassFolderPath = Path.Combine(_appdataPath, "GoodPass");
        var MKconfigPath = Path.Combine(GoodPassFolderPath, "MKconfig.txt");
        if (!System.IO.Directory.Exists(GoodPassFolderPath))
        {
            System.IO.Directory.CreateDirectory(GoodPassFolderPath);
            if (System.IO.Directory.Exists(GoodPassFolderPath))
            {
                System.IO.File.Create(MKconfigPath).Close();
                if (System.IO.File.Exists(MKconfigPath))
                {
                    System.IO.File.WriteAllText(MKconfigPath, GPHESService.getGPHES(MasterKey));
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
                    System.IO.File.WriteAllText(MKconfigPath, GPHESService.getGPHES(MasterKey));
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
                System.IO.File.WriteAllText(MKconfigPath, GPHESService.getGPHES(MasterKey));
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    public string GetLocalMKHash()
    {
        var LocalMKHash = "";
        try
        {
            LocalMKHash = File.ReadAllText(_LocalMKPath);
        }
        catch (System.IO.DirectoryNotFoundException)
        {
            _LocalMKHash = "Not found";
            LocalMKHash = "Not found";
        }
        catch (System.IO.FileNotFoundException)
        {
            _LocalMKHash = "Not found";
            LocalMKHash = "Not found";
        }
        finally
        {
            if (LocalMKHash == "")
                _LocalMKHash = "Empty";
            else
                _LocalMKHash = LocalMKHash;
        }
        return _LocalMKHash;
    }

    public string CheckMasterKey(string InputKey)
    {
        var InputKeyHash = GPHESService.getGPHES(InputKey);
        GetLocalMKHash();
        if (InputKeyHash == _LocalMKHash)
            return "pass";
        else if (_LocalMKHash == "Not found")
            return "error: not found";
        else if (_LocalMKHash == "Empty")
            return "error: data broken";
        else if (InputKeyHash != _LocalMKHash)
            return "npass";
        else return "Unknown Error";
    }

    public void ProcessMKArray(string InputKey)
    {
        App.EncryptBase = new int[40] { 1, 4, 1, 5, 9, 2, 6, 5, 3, 5, 8, 9, 7, 9, 3, 2, 3, 8, 4, 6, 2, 6, 4, 3, 3, 8, 3, 2, 7, 9, 5, 0, 2, 8, 8, 4, 1, 9, 7, 1 };
        App.MKBase = App.EncryptBase;
    }

    public async Task<string> GetLocalMKHashAsync()/*未测试*/
    {
        Task<string> LocalMKHash = taskTConverter.StringToTaskString(""); ;
        try
        {
            //使用部分同步方法用以解决异步方法不抛出异常的问题
            var tryreadfile = File.ReadAllText(_LocalMKPath);
            LocalMKHash = File.ReadAllTextAsync(_LocalMKPath);
        }
        catch (System.IO.DirectoryNotFoundException)
        {
            _LocalMKHash = "Not found";
            LocalMKHash = taskTConverter.StringToTaskString("Not found");
        }
        catch (System.IO.FileNotFoundException)
        {
            _LocalMKHash = "Not found";
            LocalMKHash = taskTConverter.StringToTaskString("Not found");
        }
        finally
        {
            if (LocalMKHash.Result == "")
                _LocalMKHash = "Empty";
            else
                _LocalMKHash = LocalMKHash.ToString();
        }
        return await LocalMKHash;
    }

    public async Task<string> CheckMasterKeyAsync(string InputKey)
    {
        var InputKeyHash = GPHESService.getGPHES(InputKey);
        var LocalMKHash = await GetLocalMKHashAsync();
        if (InputKeyHash == LocalMKHash)
            return "pass";
        else if (LocalMKHash == "Not found")
            return "error: not found";
        else if (LocalMKHash == "Empty")
            return "error: data broken";
        else if (InputKeyHash != LocalMKHash)
            return "npass";
        else return "Unknown Error";
    }
}
