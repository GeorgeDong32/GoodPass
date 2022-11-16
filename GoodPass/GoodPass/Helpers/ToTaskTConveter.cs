namespace GoodPass.Helpers;

public class TaskTConverter
{
    public async Task<string> StringToTaskString(string str)
    {
        return await Task.FromResult(str);
    }
}