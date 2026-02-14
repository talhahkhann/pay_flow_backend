using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class UtcValueConverter : ValueConverter<DateTime, DateTime>
{
    public UtcValueConverter()
        : base(
            // Convert from model to DB: ensure UTC
            v => v.Kind == DateTimeKind.Utc 
                ? v 
                : DateTime.SpecifyKind(v.ToUniversalTime(), DateTimeKind.Utc),
            // Convert from DB to model: ensure UTC
            v => v.Kind == DateTimeKind.Utc 
                ? v 
                : DateTime.SpecifyKind(v, DateTimeKind.Utc))
    { }
}