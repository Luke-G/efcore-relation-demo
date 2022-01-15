namespace example.Models;

public class User
{
    public Guid Id { get; set; }
    
    public string Username { get; set; }
    
    /// <summary>
    /// Sets up the relationship between User and its LoginHistory entities.
    /// </summary>
    public ICollection<LoginHistory> Logins { get; set; }
}