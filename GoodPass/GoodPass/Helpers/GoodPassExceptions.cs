namespace GoodPass.Helpers;

public class GPObjectNotFoundException : Exception
{
    public GPObjectNotFoundException() : base() { }
    public GPObjectNotFoundException(string message) : base(message) { }
    public GPObjectNotFoundException(string message, Exception inner) : base(message, inner) { }
}