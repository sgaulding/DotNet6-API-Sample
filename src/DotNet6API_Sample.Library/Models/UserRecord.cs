namespace DotNet6API_Sample.Library.Models;

public record UserRecord
{
    public Guid? Id { get; set; }
    public string? FistName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}