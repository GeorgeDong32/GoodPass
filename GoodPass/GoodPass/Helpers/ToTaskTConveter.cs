namespace GoodPass.Helpers;

/// <summary>
/// Convert a sync return value to async return value for some async functions
/// </summary>
public static class TaskTConverter
{
    public static async Task<string> StringToTaskString(string str)
    {
        return await Task.FromResult(str);
    }
}