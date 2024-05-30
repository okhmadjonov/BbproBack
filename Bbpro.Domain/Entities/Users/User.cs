using Bbpro.Domain.Commons;

namespace Bbpro.Domain.Entities.Users;

public class User :Auditable
{
    public string Username { get; set; }
    
    public string Phonenumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

}
