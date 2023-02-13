using GoodPass.Services;

namespace GoodPass.Models;

public class GPData
{
    /*数据*/
    public string PlatformName
    {
        get; set;
    }

    public string? PlatformUrl
    {
        get; set;
    }

    public string AccountName
    {
        get; set;
    }

    public string EncPassword
    {
        get; set;
    }

    private string DecPassword
    {
        get; set;
    }

    public DateTime LatestUpdateTime
    {
        get; set;
    }

    public string GetPassword;
    /*End 数据*/

    /*方法*/
    /// <summary>
    /// 默认GPData构造函数
    /// </summary>
    public GPData()
    {
        PlatformName = "No Name";
        PlatformUrl = null;
        AccountName = "No Name";
        DecPassword = "DecPassword";
        EncPassword = DecPassword;
        GetPassword = DecPassword;
        LatestUpdateTime = DateTime.Now;
    }

    /// <summary>
    /// GPData的拷贝构造函数
    /// </summary>
    /// <param name="data">拷贝构造的对象</param>
    public GPData(GPData data)
    {
        PlatformName = data.PlatformName;
        PlatformUrl = data.PlatformUrl;
        AccountName = data.AccountName;
        DecPassword = data.DecPassword;
        EncPassword = data.EncPassword;
        LatestUpdateTime = data.LatestUpdateTime;
        GetPassword = data.DecPassword;
    }

    /// <summary>
    /// GPData的完整构造函数
    /// </summary>
    public GPData(string platformName, string accountName, string encPassword, DateTime latestUpdateTime)
    {
        PlatformName = platformName;
        PlatformUrl = null;
        AccountName = accountName;
        EncPassword = encPassword;
        var GPCS = App.GetService<GoodPassCryptographicServices>();
        DecPassword = GPCS.DecryptStr(EncPassword);
        GetPassword = DecPassword;
        LatestUpdateTime = latestUpdateTime;
    }

    /// <summary>
    /// GPData的含时间的构造函数
    /// </summary>
    public GPData(string platformName, string? platformUrl, string accountName, string encPassword, DateTime latestUpdateTime)
    {
        PlatformName = platformName;
        PlatformUrl = platformUrl;
        AccountName = accountName;
        EncPassword = encPassword;
        var GPCS = App.GetService<GoodPassCryptographicServices>();
        DecPassword = GPCS.DecryptStr(EncPassword);
        GetPassword = DecPassword;
        LatestUpdateTime = latestUpdateTime;
    }

    /// <summary>
    /// 数据自更新，预留接口
    /// </summary>
    public void SelfUpdate() //预留接口
    {
        /*Todo:添加数据自升级的相应代码*/
        LatestUpdateTime = DateTime.Now;
    }

    /// <summary>
    /// 数据解密
    /// </summary>
    /// <returns>数据解密是否成功</returns>
    public bool DataDecrypt()
    {
        var GPCS = App.GetService<GoodPassCryptographicServices>();
        DecPassword = GPCS.DecryptStr(EncPassword);
        GetPassword = DecPassword;
        return true;
    }

    /// <summary>
    /// 更改密码
    /// </summary>
    /// <param name="newPassword">新密码</param>
    /// <returns>修改密码结果</returns>
    public string ChangePassword(string newPassword)
    {
        DataDecrypt();
        if (newPassword != DecPassword && newPassword != string.Empty && newPassword != null)
        {
            var GPCS = App.GetService<GoodPassCryptographicServices>();
            DecPassword = newPassword;
            EncPassword = GPCS.EncryptStr(newPassword);
            LatestUpdateTime = DateTime.Now;
            return "Success";
        }
        else if (newPassword == DecPassword)
        {
            return "SamePassword";
        }
        else if (newPassword == String.Empty || newPassword == null)
        {
            return "Empty";
        }
        else
        {
            return "Unknown Error";
        }
    }

    /// <summary>
    /// 修改平台Url
    /// </summary>
    /// <param name="newUrl">新的Url</param>
    /// <returns>修改结果</returns>
    public bool ChangeUrl(string? newUrl)
    {
        if (newUrl == this.PlatformUrl)
        {
            return false;
        }
        else
        {
            PlatformUrl = newUrl;
            LatestUpdateTime = DateTime.Now;
            return true;
        }
    }

    /// <summary>
    /// 修改平台名
    /// </summary>
    /// <param name="newAccountName">新的平台名</param>
    /// <returns>修改结果</returns>
    public bool ChangeAccountName(string newAccountName)
    {
        if (newAccountName == AccountName)
        {
            return false;
        }
        else
        {
            AccountName = newAccountName;
            LatestUpdateTime = DateTime.Now;
            return true;
        }
    }
    /*End 方法*/
}
