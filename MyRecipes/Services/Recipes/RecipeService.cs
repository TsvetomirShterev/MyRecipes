namespace MyRecipes.Services.Recipes
{
    using System.Collections.Generic;
    using System.Linq;

    using MyRecipes.Data;

    public class RecipeService : IRecipeService
    {
        private readonly RecipeDbContext data;

        public RecipeService(RecipeDbContext data)
        {
            this.data = data;
        }

        public RecipeQueryServiceModel All(
            string category,
            string searchTerm,
            int currentPage,
            int recipesPerPage)
        {
            var recipesQuery = this.data.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                recipesQuery = recipesQuery.Where(r => r.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                recipesQuery = recipesQuery
                    .Where(r => r.Title.ToLower().Contains(searchTerm.ToLower())
                    || r.Category.Name.ToLower().Contains(searchTerm.ToLower())
                    || r.Instructions.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalRecipes = recipesQuery.Count();

            var recipes = recipesQuery
                .OrderByDescending(r => r.Id)
                .Skip((currentPage - 1) * recipesPerPage)
                .Take(recipesPerPage)
                .Select(r => new RecipeServiceModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl,
                    PrepTime = r.PrepTime,
                    CookingTime = r.CookingTime,
                    PortionsCount = r.PortionsCount,
                    Category = r.Category.Name,
                })
                .ToList();

            return new RecipeQueryServiceModel
            {
                TotalRecipes = totalRecipes,
                CurrentPage = currentPage,
                RecipesPerPage = recipesPerPage,
                Recipes = recipes,
            };
        }

        public IEnumerable<string> AllRecipeCategories()
          => this.data
                 .Recipes
                 .Select(r => r.Category.Name)
                 .Distinct()
                 .ToArray();
    }
}
