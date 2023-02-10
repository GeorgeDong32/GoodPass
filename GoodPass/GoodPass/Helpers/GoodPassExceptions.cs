namespace GoodPass.Helpers;

/// <summary>
/// A class to provide GoodPass Exception -- Object not found in collection
/// </summary>
public class GPObjectNotFoundException : Exception
{
    public GPObjectNotFoundException() : base() { }
    public GPObjectNotFoundException(string message) : base(message) { }
    public GPObjectNotFoundException(string message, Exception inner) : base(message, inner) { }
}


public class GPRuntimeException : Exception
{
    public GPRuntimeException() : base() { }
    public GPRuntimeException(string message) : base(message) { }
    public GPRuntimeException(string message, Exception inner) : base(message, inner) { }
}