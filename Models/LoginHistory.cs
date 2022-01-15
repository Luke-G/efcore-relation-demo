using System.Text.Json.Serialization;

namespace example.Models;

/// <summary>
/// An EFCore entity which will be linked to a User.
/// </summary>
public class LoginHistory
{
    /// <summary>
    /// The ID for the LoginHistory, which will be the primary key.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The country foreign key on the LoginHistory model.
    /// </summary>
    public Guid? CountryId { get; set; }

    /// <summary>
    /// The Country relation, linked by the Country ID.
    /// </summary>
    public Country? Country { get; set; }


    
    /// <summary>
    /// The user foreign key on the LoginHistory model.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// The User relation, linked by the User ID (foreign key).
    /// </summary>
    [JsonIgnore]
    public User User { get; set; }
    
    public DateTime Time { get; set; }
    public string Ip { get; set; }
}