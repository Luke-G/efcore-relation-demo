namespace example.Models;

/// <summary>
/// A data transfer object representing what we want to show to the user regarding the LoginHistory.
/// The properties are either manually mapped in a query, or AutoMapper can do it for us.
/// </summary>
public class LoginHistoryResponse
{
    public DateTime Time { get; set; }
    
    public string Ip { get; set; }
    
    public string? CountryName { get; set; }
}