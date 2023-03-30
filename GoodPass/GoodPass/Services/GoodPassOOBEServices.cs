using GoodPass.Helpers;
using GoodPass.Models;
using Windows.Storage;

namespace GoodPass.Services;

/// <summary>
/// Provide GoodPass Out-Of-Box-Experience Services
/// </summary>
public static class OOBEServices
{
    /// <summary>
    /// 获取OOBE状态
    /// </summary>
    /// <param name="oobePosition">OOBE位置，如MainOOBE/AddDataOOBE</param>
    public static async Task<OOBESituation> GetOOBEStatusAsync(string oobePosition)
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
                return OOBESituation.EnableOOBE;
            case "DIsableOOBE":
                return OOBESituation.DIsableOOBE;
            default:
                return OOBESituation.EnableOOBE;
        }
    }

    public static async Task<bool> SetOOBEStatusAsync(string oobePosition, OOBESituation oobeSituation)
    {
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