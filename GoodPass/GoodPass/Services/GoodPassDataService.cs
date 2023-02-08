using GoodPass.Models;

namespace GoodPass.Services;

public class GoodPassDataService
{
    /// <summary>
    /// 从DataManager获取IEnumerable形式的所有数据或初始化示例数据
    /// </summary>
    /// <returns>IEnumerable形式的数据列表</returns>
    private static IEnumerable<GPData> AllDatas()
    {
        var manager = App.DataManager;
        manager.DecryptAllDatas();
        var datas = manager.GetAllDatas();
        if (datas != null && datas.Count() != 0)
        {
            return datas;
        }
        else
        {
            datas = new List<GPData>()
            {
                //常规代码
                //new GPData("Sample", "https://github.com/GeorgeDong32/GoodPass", "SampleAccount", App.GetService<GoodPassCryptographicServices>().EncryptStr("SamplePassword"), DateTime.Now)
                //测试代码2023.1.13
                new GPData("Test", "https://github.com/GeorgeDong32/GoodPass-v3", "001", App.GetService<GoodPassCryptographicServices>().EncryptStr("Test"), DateTime.Now),
                new GPData("Test", "https://github.com/GeorgeDong32/GoodPass", "002", App.GetService<GoodPassCryptographicServices>().EncryptStr("Test"), DateTime.Now),
                new GPData("Test", String.Empty ,"003", App.GetService<GoodPassCryptographicServices>().EncryptStr("Test"), DateTime.Now)
            };
            foreach (var data in datas)
            {
                data.DataDecrypt();
                manager.AddData(data);
            }
            return datas;
        }
    }

    /// <summary>
    /// 异步获取IEnumerable形式的所有数据或初始化示例数据
    /// </summary>
    /// <returns>异步的IEnumerable形式的数据列表</returns>
    public async Task<IEnumerable<GPData>> GetListDetailsDataAsync()
    {
        var _allDatas = new List<GPData>(AllDatas());
        await Task.CompletedTask;
        return _allDatas;
    }
}
