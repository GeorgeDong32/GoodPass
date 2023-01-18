using GoodPass.Services;

namespace GoodPass.Models;

public class GPManager
{
    /*成员区*/
    private List<GPData> GPDatas;

    /*方法区*/
    public GPManager()
    {
        GPDatas = new List<GPData>();
    }

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

    public int AccurateSearch(string platformName, string accountName) //预留接口（精确搜索）
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

    public bool AddData(string platformName, string platformUrl, string accountName, string password)
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
        var cryptService = new GoodPassCryptographicServices();
        var encPassword = cryptService.EncryptStr(password);
        var datatemp = new GPData(platformName, platformUrl, accountName, encPassword, DateTime.Now);
        GPDatas.Add(datatemp);
        return true;
    }

    public bool AddData(string platformName, string platformUrl, string accountName, string encPassword, DateTime latestUpdateTime)/*自动添加数据*/
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

    public bool DeleteData(string platformName, string accountName)/*删除数据*/
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

    public string ChangeData(string platformName, string accountName, string newPassword)//重新设置密码
    {
        var targetIndex = AccurateSearch(platformName, accountName);
        return GPDatas[targetIndex].ChangePassword(newPassword);
    }

    public bool LoadFormFile(string filePath)//从文件导入数据
    {
        if (File.Exists(filePath))
        {
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
            File.Create(filePath);
            return false;
        }
    }

    //Todo:出现文件被GoodPass某一进程占用情况
    public bool SaveToFile(string filePath)//保存数据到文件
    {
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, "PlatformName,PlatformUrl,AccountName,EncPassword,LatestUpdateTime");
            foreach (var data in GPDatas)
            {
                File.AppendAllText(filePath, $"{data.PlatformName},{data.PlatformUrl},{data.AccountName},{data.EncPassword},{data.LatestUpdateTime}\n", System.Text.Encoding.UTF8);
            }
            return true;
        }
        else
        {
            File.Create(filePath);
            File.WriteAllText(filePath, "PlatformName,PlatformUrl,AccountName,EncPassword,LatestUpdateTime");
            foreach (var data in GPDatas)
            {
                File.AppendAllText(filePath, $"{data.PlatformName},{data.PlatformUrl},{data.AccountName},{data.EncPassword},{data.LatestUpdateTime}\n", System.Text.Encoding.UTF8);
            }
            return true;
        }
    }

    public IEnumerable<GPData> GetAllDatas()
    {
        return GPDatas;
    }

    public void DecryptAllDatas()
    {
        foreach (var data in GPDatas)
        {
            data.DataDecrypt();
        }
    }

    public GPData GetData(int index)
    {
        if (index == -1 || index > GPDatas.Count)
            return null;
        else
            return GPDatas[index];
    }

    public GPData GetData(string platformName, string accountName)
    {
        var targetIndex = AccurateSearch(platformName, accountName);
        if (targetIndex == -1 || targetIndex > GPDatas.Count)
            return null;
        else
            return GPDatas[targetIndex];
    }
}
