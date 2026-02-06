namespace PaymentSystem.Application.Common.Models;

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(T? value, bool isSuccess, Error error) 
        : base(isSuccess, error)
    {
        Value = value;
    }

    private Result(T? value, bool isSuccess, List<Error> errors) 
        : base(isSuccess, errors)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(value, true, Error.None);

    public new static Result<T> Failure(Error error) => new(default, false, error);

    public new static Result<T> Failure(string code, string message) 
        => new(default, false, new Error(code, message));

    public new static Result<T> Failure(List<Error> errors) 
        => new(default, false, errors);

    public static Result<T> ValidationFailure(List<ValidationError> errors) 
        => new(default, false, errors.Cast<Error>().ToList());

    public static Result<T> NotFound(string code, string message)
        => new(default, false, Error.NotFound(code, message));

    public static Result<T> Unauthorized(string code, string message)
        => new(default, false, Error.Unauthorized(code, message));

    public static Result<T> Conflict(string code, string message)
        => new(default, false, Error.Conflict(code, message));

    public static implicit operator Result<T>(T value) => Success(value);
}