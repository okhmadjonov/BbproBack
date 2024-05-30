namespace Bbpro.Service.Exceptions;

public class BbproException : Exception
{
    public int Code { get; set; }
    public bool? Global { get; set; }

    public BbproException(int code, string message, bool? global = true) : base(message)
    {

        Code = code;
        Global = global;
    }
}

