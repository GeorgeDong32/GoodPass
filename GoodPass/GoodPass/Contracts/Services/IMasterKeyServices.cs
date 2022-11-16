namespace GoodPass.Contracts.Services;

public interface IMaterKeyService
{
    string GetLocalMKHash();

    string CheckMasterKey(string InputKey);

    void ProcessMKArray(string InputKey);

    Task<string> GetLocalMKHashAsync();

    Task<string> CheckMasterKeyAsync(string InputKey);
}