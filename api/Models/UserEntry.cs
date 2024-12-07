namespace api.Models;

public class UserEntry
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? HashedPassword { get; set; }
}