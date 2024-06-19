using Bbpro.Domain.Entities.About;
using Bbpro.Domain.Entities.Brands;
using Bbpro.Domain.Entities.Categories;
using Bbpro.Domain.Entities.Latests;
using Bbpro.Domain.Entities.MainContact;
using Bbpro.Domain.Entities.Orders;
using Bbpro.Domain.Entities.Projects;
using Bbpro.Domain.Entities.Solutions;
using Bbpro.Infrastructure.Configurations;
using Bbpro.Infrastructure.Configurations.LanguageConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Bbpro.Infrastructure.Contexts;

public class BbproDbContext : DbContext
{
    public BbproDbContext(DbContextOptions<BbproDbContext> options): base(options)
    { }


    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryConnectSolution> CategoryConnectSolution { get; set; }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Latest> Latests { get; set; }
    public DbSet<Solution> Solutions { get; set; }
    public DbSet<Project> Projects { get; set; }

    public DbSet<About> Abouts { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Order> Orders { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

        modelBuilder.Entity<Category>()
         .HasMany(x => x.Solutions)
         .WithOne(y => y.Category)
         .HasForeignKey(y => y.CategoryId);

        modelBuilder.Entity<Solution>()
            .HasMany<CategoryConnectSolution>()
            .WithOne(t => t.Solution)
            .HasForeignKey(t => t.SolutionId);



        /*------------------------------Language Configurations--------------------------------*/

        AboutLanguageConfig.Configure(modelBuilder);
        ContactLanguageConfig.Configure(modelBuilder);
        SolutionLanguageConfig.Configure(modelBuilder);
        ProjectLanguageConfig.Configure(modelBuilder);
        LatestLanguageConfig.Configure(modelBuilder);
        CategoryLanguageConfig.Configure(modelBuilder);
    }
}
