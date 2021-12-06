namespace MyRecipes.Infrastrucutre.Extentions
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MyRecipes.Data;
    using MyRecipes.Data.Models;

    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            
            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<RecipeDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<RecipeDbContext>();

            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
           {
                new Category {Name = "Breakfast "},
                new Category {Name = "Lunch"},
                new Category {Name = "Dinner"},
                new Category {Name = "Appetizer"},
                new Category {Name = "Salad"},
                new Category {Name = "Main-course"},
                new Category {Name = "Side-dish"},
                new Category {Name = "Baked-goods"},
                new Category {Name = "Dessert"},
                new Category {Name = "Soup"},
            });

            data.SaveChanges();
        }
    }
}
