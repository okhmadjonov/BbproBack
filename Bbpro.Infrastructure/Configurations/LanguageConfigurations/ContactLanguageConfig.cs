using Bbpro.Domain.Entities.MainContact;
using Microsoft.EntityFrameworkCore;

namespace Bbpro.Infrastructure.Configurations.LanguageConfigurations;


public class ContactLanguageConfig
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.OwnsOne(s => s.Address, title =>
            {
                title.Property(t => t.RU).HasColumnName("Address_RU");
                title.Property(t => t.UZ).HasColumnName("Address_UZ");
                title.Property(t => t.EN).HasColumnName("Address_EN");
            });

            entity.OwnsOne(s => s.WorkDay, description =>
            {
                description.Property(d => d.RU).HasColumnName("WorkDay_RU");
                description.Property(d => d.UZ).HasColumnName("WorkDay_UZ");
                description.Property(d => d.EN).HasColumnName("WorkDay_EN");
            });

            entity.OwnsOne(s => s.Weekend, description =>
            {
                description.Property(d => d.RU).HasColumnName("Weekend_RU");
                description.Property(d => d.UZ).HasColumnName("Weekend_UZ");
                description.Property(d => d.EN).HasColumnName("Weekend_EN");
            });
        });
    }
}
