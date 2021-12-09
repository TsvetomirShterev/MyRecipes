namespace MyRecipes.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MyRecipes.Data.Models;

    public class RecipeDbContext : IdentityDbContext<User>
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<Chef> Chefs { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Chef>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Chef>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
