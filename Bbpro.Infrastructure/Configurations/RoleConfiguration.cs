using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Bbpro.Domain.Entities.Roles;

namespace Bbpro.Infrastructure.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(DefaultRoles);
    }
    private Role[] DefaultRoles = new[]
    {
        new Role("SuperAdmin")
        {
            Id = 1,
            CreatedAt = new DateTime(2024, 5, 30, 16, 13, 56, 461, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2024, 5, 30, 16, 13, 56, 461, DateTimeKind.Utc),
            IsActive = true
        },

    };

}
