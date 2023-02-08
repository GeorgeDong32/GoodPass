namespace GoodPass.Models;

/// <summary>
/// AddDataDialog的处理结果
/// </summary>
public enum AddDataResult
{
    Success,
    Failure_Duplicate,
    Failure,
    Undetermined
}

/// <summary>
/// EditDataDialog的处理结果
/// </summary>
public enum EditDataResult
{
    Success,
    Failure,
    UnknowError,
    Nochange
}

public enum OOBESituation
{
    EnableOOBE,
    DIsableOOBE
}

public enum AgreeStatus
{
    Agree,
    NotAgree
}