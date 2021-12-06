namespace MyRecipes.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MyRecipes.Data.Models;

    public class RecipeDbContext : IdentityDbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; init; }

        public DbSet<Category> Categories { get; init; }
    }
}
