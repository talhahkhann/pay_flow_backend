namespace PaymentSystem.Application.Common.Models;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public List<Error> Errors { get; }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new InvalidOperationException("Success result cannot have an error");

        if (!isSuccess && error == Error.None)
            throw new InvalidOperationException("Failure result must have an error");

        IsSuccess = isSuccess;
        Error = error;
        Errors = new List<Error>();
    }

    protected Result(bool isSuccess, List<Error> errors)
    {
        IsSuccess = isSuccess;
        Error = errors.FirstOrDefault() ?? Error.None;
        Errors = errors ?? new List<Error>();
    }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result Failure(string code, string message) 
        => new(false, new Error(code, message));

    public static Result Failure(List<Error> errors) => new(false, errors);

    public static Result ValidationFailure(List<ValidationError> errors) 
        => new(false, errors.Cast<Error>().ToList());
}