namespace Bulk.Model.Actors;

public class ActorCreateModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "Admin";
}
