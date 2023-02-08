using GoodPass.Contracts.Services;
using GoodPass.Helpers;
using GoodPass.Models;
using Windows.Storage;

namespace GoodPass.Services;

/// <summary>
/// Provide GoodPass Out-Of-Box-Experience Services
/// </summary>
public class OOBEServices
{
    private readonly ILocalSettingsService _localSettingsService;

    private OOBESituation _OOBESituation
    {
        get; set;
    }

    public OOBEServices(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;
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
                _OOBESituation = OOBESituation.EnableOOBE;
                break;
            case "DIsableOOBE":
                _OOBESituation = OOBESituation.DIsableOOBE;
                break;
            default:
                _OOBESituation = OOBESituation.EnableOOBE;
                break;
        }
        return _OOBESituation;
    }

    public async Task<bool> SetOOBEStatusAsync(string oobePosition, OOBESituation oobeSituation)
    {
        _OOBESituation = oobeSituation;
        switch (oobeSituation)
        {
            case OOBESituation.EnableOOBE:
                if (RuntimeHelper.IsMSIX)
                {
                    ApplicationData.Current.LocalSettings.Values[oobePosition] = oobeSituation.ToString();
                    await Task.CompletedTask;
                }
                return true;
            case OOBESituation.DIsableOOBE:
                if (RuntimeHelper.IsMSIX)
                {
                    ApplicationData.Current.LocalSettings.Values[oobePosition] = oobeSituation.ToString();
                    await Task.CompletedTask;
                }
                return true;
            default:
                return false;
        }
    }
}