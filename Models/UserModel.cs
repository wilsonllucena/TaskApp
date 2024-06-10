namespace TaskApp.Models;

public class UserModel
{   
    public int Id { get; set; }
    
    public Guid PublicId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public bool Active { get; set;  }
}