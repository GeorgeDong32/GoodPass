using GoodPass.Helpers;
using GoodPass.Models;
using Windows.Storage;

namespace GoodPass.Services;

/// <summary>
/// Provide GoodPass Out-Of-Box-Experience Services
/// </summary>
public class OOBEServices
{
    private OOBESituation OOBESituation
    {
        get; set;
    }

    public OOBEServices()
    {
    }

    /// <summary>
    /// 获取OOBE状态
    /// </summary>
    /// <param name="oobePosition">OOBE位置，如MainOOBE/AddDataOOBE</param>
    public async Task<OOBESituation> GetOOBEStatusAsync(string oobePosition)
    {
        var loaclstatus = "";
        if (RuntimeHelper.IsMSIX)
        {
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(oobePosition, out var obj))
            {
                loaclstatus = (string)obj;
                await Task.CompletedTask;
            }
        }
        switch (loaclstatus)
        {
            case "EnableOOBE":
                OOBESituation = OOBESituation.EnableOOBE;
                break;
            case "DIsableOOBE":
                OOBESituation = OOBESituation.DIsableOOBE;
                break;
            default:
                OOBESituation = OOBESituation.EnableOOBE;
                break;
        }
        return OOBESituation;
    }

    public async Task<bool> SetOOBEStatusAsync(string oobePosition, OOBESituation oobeSituation)
    {
        OOBESituation = oobeSituation;
        if (RuntimeHelper.IsMSIX)
        {
            ApplicationData.Current.LocalSettings.Values[oobePosition] = oobeSituation.ToString();
            await Task.CompletedTask;
            return true;
        }
        else
        {
            throw new GPRuntimeException("SetOOBEStatusAsync: Not in MSIX");
        }
    }
}