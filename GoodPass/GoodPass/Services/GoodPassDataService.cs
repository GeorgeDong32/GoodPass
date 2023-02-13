using GoodPass.Models;

namespace GoodPass.Services;

public static class GoodPassDataService
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
                //生成示例
                new GPData("Example", "https://github.com/GeorgeDong32/GoodPass", "example1@example.com", App.GetService<GoodPassCryptographicServices>().EncryptStr("ExamplePassword"), DateTime.Now),
                new GPData("Example", "https://example.com", "example2", App.GetService<GoodPassCryptographicServices>().EncryptStr("ExamplePassword"), DateTime.Now),
                new GPData("Example", String.Empty ,"404871381511007", App.GetService<GoodPassCryptographicServices>().EncryptStr("ExamplePassword"), DateTime.Now)
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
    public static async Task<IEnumerable<GPData>> GetListDetailsDataAsync()
    {
        var _allDatas = new List<GPData>(AllDatas());
        await Task.CompletedTask;
        return _allDatas;
    }
}
