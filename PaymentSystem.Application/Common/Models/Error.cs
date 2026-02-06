namespace PaymentSystem.Application.Common.Models;

public class Error
{
    public string Code { get; set; }
    public string Message { get; set; }
    public ErrorType Type { get; set; }

    public Error(string code, string message, ErrorType type = ErrorType.Failure)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static Error None => new(string.Empty, string.Empty, ErrorType.None);
    public static Error NullValue => new("Error.NullValue", "Null value was provided", ErrorType.Failure);
    
    public static Error Validation(string code, string message) 
        => new(code, message, ErrorType.Validation);
    
    public static Error NotFound(string code, string message) 
        => new(code, message, ErrorType.NotFound);
    
    public static Error Conflict(string code, string message) 
        => new(code, message, ErrorType.Conflict);
    
    public static Error Unauthorized(string code, string message) 
        => new(code, message, ErrorType.Unauthorized);
}

public enum ErrorType
{
    None = 0,
    Failure = 1,
    Validation = 2,
    NotFound = 3,
    Conflict = 4,
    Unauthorized = 5
}