using Bbpro.Domain.Entities.Latests;
using Microsoft.EntityFrameworkCore;

namespace Bbpro.Infrastructure.Configurations.LanguageConfigurations;

public class LatestLanguageConfig
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Latest>(entity =>
        {
            entity.OwnsOne(s => s.Title, title =>
            {
                title.Property(t => t.RU).HasColumnName("Title_RU");
                title.Property(t => t.UZ).HasColumnName("Title_UZ");
                title.Property(t => t.EN).HasColumnName("Title_EN");
            });

            entity.OwnsOne(s => s.Description, description =>
            {
                description.Property(d => d.RU).HasColumnName("Description_RU");
                description.Property(d => d.UZ).HasColumnName("Description_UZ");
                description.Property(d => d.EN).HasColumnName("Description_EN");
            });
        });
    }
}
