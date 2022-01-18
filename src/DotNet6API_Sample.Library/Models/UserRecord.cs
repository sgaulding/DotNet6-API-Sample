namespace DotNet6API_Sample.Library.Models;

public record UserRecord
{
    public Guid? ID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
}