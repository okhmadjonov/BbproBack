using Bbpro.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Bbpro.Infrastructure.Extentions;

namespace Bbpro.Infrastructure.Configurations;


internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(DefaultUserAdmin);

    }

    private User DefaultUserAdmin =>
        new User()
        {
            Id = 1,
            Username = "Admin",
            Email = "admin@gmail.com",
            Phonenumber = "+99898 000 00 00",
            Password = "Admin@123?".Encrypt(),
            CreatedAt = new DateTime(2024, 5, 30, 16, 13, 56, 461, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2024, 5, 30, 16, 13, 56, 461, DateTimeKind.Utc),
        };

}
