using GoodPass.Services;
namespace GoodPass.Models;

public class GPManager
{
    /*成员区*/
    private List<GPData> GPDatas;
    /*End 成员区*/

    /*方法区*/
    /// <summary>
    /// 构造函数
    /// </summary>
    public GPManager()
    {
        GPDatas = new List<GPData>();
    }

    /// <summary>
    /// 数据模糊搜索
    /// </summary>
    /// <param name="platformName">模糊搜索平台名</param>
    /// <returns>搜索结果index数组</returns>
    public int[] FuzzySearch(string platformName) //预留接口（模糊搜索）
    {
        var indexArray = new int[1] { -1 };
        var indexArrayCount = 0;
        foreach (var data in GPDatas)
        {
            if (data.PlatformName == platformName)
            {
                indexArray[indexArrayCount] = GPDatas.IndexOf(data);
                indexArrayCount++;
                Array.Resize(ref indexArray, indexArray.Length + 1);
            }
        }
        return indexArray;
    }

    /// <summary>
    /// 精确搜索
    /// </summary>
    /// <param name="platformName">平台名</param>
    /// <param name="accountName">账户名</param>
    /// <returns>搜索结果index</returns>
    public int AccurateSearch(string platformName, string accountName)
    {
        foreach (var data in GPDatas)
        {
            if (data.PlatformName == platformName && data.AccountName == accountName)
            {
                return GPDatas.IndexOf(data);
            }
        }
        return -1;
    }

    /// <summary>
    /// 搜索框搜索接口
    /// </summary>
    /// <param name="searchText"></param>
    /// <returns></returns>
    public List<GPData> SuggestSearch(string searchText)
    {
        var matchString = searchText.ToLower();
        var query = from GPData data in GPDatas
                    where data.PlatformName.ToLower().Contains(matchString) || data.AccountName.ToLower().Contains(matchString)
                    select data;
        return query.ToList();
    }

    /// <summary>
    /// 添加数据1(自带去重)
    /// </summary>
    /// <returns>添加结果</returns>
    public bool AddData(string platformName, string? platformUrl, string accountName, string password)
    {
        var indexArray = FuzzySearch(platformName);
        foreach (var index in indexArray)
        {
            if (index == -1)
            {
                break;
            }
            if (GPDatas[index].AccountName == accountName)
            {
                return false;
            }
        }
        var encPassword = GoodPassCryptographicServices.EncryptStr(password);
        var datatemp = new GPData(platformName, platformUrl, accountName, encPassword, DateTime.Now);
        GPDatas.Add(datatemp);
        return true;
    }

    /// <summary>
    /// 添加数据2(自带去重)
    /// </summary>
    /// <returns>添加结果</returns>
    public bool AddData(string platformName, string? platformUrl, string accountName, string encPassword, DateTime latestUpdateTime)/*自动添加数据*/
    {
        var indexArray = FuzzySearch(platformName);
        foreach (var index in indexArray)
        {
            if (index == -1)
            {
                break;
            }
            if (GPDatas[index].AccountName == accountName)
            {
                return false;
            }
        }
        var datatemp = new GPData(platformName, platformUrl, accountName, encPassword, latestUpdateTime);
        GPDatas.Add(datatemp);
        return true;
    }

    /// <summary>
    /// 添加数据3(自带去重)
    /// </summary>
    /// <returns>添加结果</returns>
    public bool AddData(GPData data)
    {
        var indexArray = FuzzySearch(data.PlatformName);
        foreach (var index in indexArray)
        {
            if (index == -1)
            {
                break;
            }
            if (GPDatas[index].AccountName == data.AccountName)
            {
                return false;
            }
        }
        GPDatas.Add(data);
        return true;
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="platformName">目标平台名</param>
    /// <param name="accountName">目标账号名</param>
    /// <returns>删除结果</returns>
    public bool DeleteData(string platformName, string accountName)
    {
        var indexArray = FuzzySearch(platformName);
        foreach (var index in indexArray)
        {
            if (GPDatas[index].AccountName == accountName)
            {
                GPDatas.RemoveAt(index);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 更改密码
    /// </summary>
    /// <param name="platformName">目标平台名</param>
    /// <param name="accountName">目标账号名</param>
    /// <param name="newPassword">新密码</param>
    /// <returns>更改结果</returns>
    public string ChangePassword(string platformName, string accountName, string newPassword)//重新设置密码
    {
        var targetIndex = AccurateSearch(platformName, accountName);
        return GPDatas[targetIndex].ChangePassword(newPassword);
    }

    public void SelfUpdate()
    {
        foreach (var data in GPDatas)
        {
            data.SelfUpdate();
        }
    }

    /// <summary>
    /// 更改平台Url
    /// </summary>
    /// <param name="platformName">目标平台名</param>
    /// <param name="accountName">目标账号名</param>
    /// <param name="newUrl">新Url</param>
    /// <returns>更改结果</returns>
    public bool ChangeUrl(string platformName, string accountName, string newUrl)
    {
        var targetIndex = AccurateSearch(platformName, accountName);
        if (targetIndex == -1)
        {
            return false;
        }
        else
        {
            return GPDatas[targetIndex].ChangeUrl(newUrl);
        }
    }

    /// <summary>
    /// 更改账号名(自动去重)
    /// </summary>
    /// <param name="platformName">目标平台名</param>
    /// <param name="accountName">目标账号名</param>
    /// <param name="newAccountName">新账号名</param>
    /// <returns>更改结果</returns>
    public bool ChangeAccountName(string platformName, string accountName, string newAccountName)
    {
        var targetIndex = AccurateSearch(platformName, accountName);
        if (targetIndex == -1)
        {
            return false;
        }
        else
        {
            GPDatas[targetIndex].ChangeAccountName(newAccountName);
            if (GPDatas[targetIndex].AccountName == newAccountName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 更改平台名
    /// </summary>
    /// <param name="platformName">目标平台名</param>
    /// <param name="accountName">目标账号名</param>
    /// <param name="newPlatformName">新平台名</param>
    /// <returns>更改结果</returns>
    public bool ChangePlatformName(string platformName, string accountName, string newPlatformName)
    {
        if (platformName == newPlatformName)
            return false;
        var targetIndex = AccurateSearch(platformName, accountName);
        if (targetIndex == -1)
        {
            return false;
        }
        else
        {
            GPDatas[targetIndex].DataDecrypt();
            var password = GPDatas[targetIndex].GetPassword;
            var platformUrl = GPDatas[targetIndex].PlatformUrl;
            DeleteData(platformName, accountName);
            return AddData(newPlatformName, platformUrl, accountName, password);
        }
    }

    /// <summary>
    /// 从本地数据文件加载数据
    /// </summary>
    /// <param name="filePath">数据文件路径</param>
    /// <returns>加载结果</returns>
    public bool LoadFormFile(string filePath)//从文件导入数据
    {
        var userName = Environment.UserName;
        var appdataPath = $"C:\\Users\\{userName}\\AppData\\Local";
        var gpFolderPath = Path.Combine(appdataPath, "GoodPass");
        if (File.Exists(filePath))
        {
            if (GPDatas.Count != 0)
                GPDatas.Clear();
            var dataLines = File.ReadLines(filePath);
            dataLines = dataLines.Skip(1); //跳过文件头
            foreach (var line in dataLines)
            {
                var data = line.Split(',');
                AddData(data[0], data[1], data[2], data[3], DateTime.Parse(data[4]));
            }
            return true;
        }
        else
        {
            if (Directory.Exists(gpFolderPath))
            {
                File.Create(filePath).Close();
                return true;
            }
            else
            {
                Directory.CreateDirectory(gpFolderPath);
                if (System.IO.Directory.Exists(gpFolderPath))
                {
                    System.IO.File.Create(filePath).Close();
                    if (System.IO.File.Exists(filePath))
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Failed to create config file!");
                    }
                }
                else
                {
                    throw new Exception("Failed to create config file!");
                }
            }
        }
    }

    /// <summary>
    /// 保存数据到本地文件
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>保存结果</returns>
    public async Task<bool> SaveToFileAsync(string filePath)//保存数据到文件
    {
        if (File.Exists(filePath))
        {
            await File.WriteAllTextAsync(filePath, "PlatformName,PlatformUrl,AccountName,EncPassword,LatestUpdateTime\n", System.Text.Encoding.UTF8);
            foreach (var data in GPDatas)
            {
                await File.AppendAllTextAsync(filePath, $"{data.PlatformName},{data.PlatformUrl},{data.AccountName},{data.EncPassword},{data.LatestUpdateTime}\n", System.Text.Encoding.UTF8);
            }
            return true;
        }
        else
        {
            File.Create(filePath).Close();
            await File.WriteAllTextAsync(filePath, "PlatformName,PlatformUrl,AccountName,EncPassword,LatestUpdateTime\n", System.Text.Encoding.UTF8);
            foreach (var data in GPDatas)
            {
                await File.AppendAllTextAsync(filePath, $"{data.PlatformName},{data.PlatformUrl},{data.AccountName},{data.EncPassword},{data.LatestUpdateTime}\n", System.Text.Encoding.UTF8);
            }
            return true;
        }
    }

    public async Task<bool> SavePlaintextToFile(string filePath)
    {
        this.DecryptAllDatas();
        if (File.Exists(filePath))
        {
            await File.WriteAllTextAsync(filePath, "PlatformName,PlatformUrl,AccountName,Password,LatestUpdateTime\n", System.Text.Encoding.UTF8);
            foreach (var data in GPDatas)
            {
                await File.AppendAllTextAsync(filePath, $"{data.PlatformName},{data.PlatformUrl},{data.AccountName},{data.GetPassword},{data.LatestUpdateTime}\n", System.Text.Encoding.UTF8);
            }
            return true;
        }
        else
        {
            File.Create(filePath).Close();
            await File.WriteAllTextAsync(filePath, "PlatformName,PlatformUrl,AccountName,Password,LatestUpdateTime\n", System.Text.Encoding.UTF8);
            foreach (var data in GPDatas)
            {
                await File.AppendAllTextAsync(filePath, $"{data.PlatformName},{data.PlatformUrl},{data.AccountName},{data.GetPassword},{data.LatestUpdateTime}\n", System.Text.Encoding.UTF8);
            }
            return true;
        }
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns>IEnumerable形式的所有数据</returns>
    public IEnumerable<GPData> GetAllDatas()
    {
        return GPDatas;
    }

    /// <summary>
    /// 解密所有数据
    /// </summary>
    public void DecryptAllDatas()
    {
        foreach (var data in GPDatas)
        {
            data.DataDecrypt();
        }
    }

    public void EncryptAllDatas()
    {
        foreach (var data in GPDatas)
        {
            data.DataEncrypt();
        }
    }

    /// <summary>
    /// 获取指定数据
    /// </summary>
    /// <param name="index">目标index</param>
    /// <returns>指定数据</returns>
    public GPData? GetData(int index)
    {
        if (index == -1 || index > GPDatas.Count)
            return null;
        else
            return GPDatas[index];
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <param name="platformName">目标平台名</param>
    /// <param name="accountName">目标账号名</param>
    /// <returns>指定数据</returns>
    public GPData? GetData(string platformName, string accountName)
    {
        var targetIndex = AccurateSearch(platformName, accountName);
        if (targetIndex == -1 || targetIndex > GPDatas.Count)
            return null;
        else
            return GPDatas[targetIndex];
    }
    /*End 方法区*/
}
