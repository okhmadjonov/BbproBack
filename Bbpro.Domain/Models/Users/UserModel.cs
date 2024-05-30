using Bbpro.Domain.Entities.Users;

namespace Bbpro.Domain.Models.Users;

public class UserModel
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Username { get; set; }
    public string Phonenumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public virtual UserModel MapFromEntity(User entity)
    {
        Id = entity.Id;
        CreatedAt = entity.CreatedAt;
        UpdatedAt = entity.UpdatedAt == DateTime.MinValue ? null : entity.UpdatedAt;
        Username = entity.Username;
        Email = entity.Email;
        Phonenumber = entity.Phonenumber;
        Password = entity.Password;
        return this;
    }

}
