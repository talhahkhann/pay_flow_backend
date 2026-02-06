namespace PaymentSystem.Application.Common.Models;

public class ValidationError : Error
{
    public string PropertyName { get; set; }

    public ValidationError(string propertyName, string message) 
        : base("Validation.Error", message, ErrorType.Validation)
    {
        PropertyName = propertyName;
    }

    public static ValidationError Create(string propertyName, string message)
        => new(propertyName, message);
}