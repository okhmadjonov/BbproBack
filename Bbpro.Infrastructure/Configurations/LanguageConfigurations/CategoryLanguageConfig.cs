using Bbpro.Domain.Entities.About;
using Bbpro.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;

namespace Bbpro.Infrastructure.Configurations.LanguageConfigurations;

public class CategoryLanguageConfig
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.OwnsOne(s => s.Title, title =>
            {
                title.Property(t => t.RU).HasColumnName("Title_RU");
                title.Property(t => t.UZ).HasColumnName("Title_UZ");
                title.Property(t => t.EN).HasColumnName("Title_EN");
            });
        });
    }
}
