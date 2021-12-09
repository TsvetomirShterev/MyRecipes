namespace MyRecipes.Services.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyRecipes.Data;
    using MyRecipes.Data.Models;
    using MyRecipes.Models.Home;
    using MyRecipes.Models.Recipes;

    public class RecipeService : IRecipeService
    {
        private readonly RecipeDbContext data;
        private readonly IMapper mapper;

        public RecipeService(RecipeDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
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

            var recipes = this.GetRecipes(recipesQuery
                .OrderByDescending(r => r.Id)
                .Skip((currentPage - 1) * recipesPerPage)
                .Take(recipesPerPage));


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

        public IEnumerable<RecipeServiceModel> ByUser(string userId)
            => this
            .GetRecipes(this.data.Recipes
                .Where(c => c.Chef.UserId == userId));


        public RecipesInstructionsServiceModel Details(int id)
        => this.data
            .Recipes
            .Where(r => r.Id == id)
            .ProjectTo<RecipesInstructionsServiceModel>((this.mapper.ConfigurationProvider))
            .FirstOrDefault();

        public bool CategoryExists(int categoryId)
          => this.data
            .Categories
            .Any(c => c.Id == categoryId);

        public int CreateRecipe(string title, string ingredients, string instructions, string imageUrl, int portionsCount, int prepTime, int cookingTime, int categoryId, int chefId)
        {
            var validRecipe = new Recipe
            {
                Title = title,
                Ingredients = ingredients,
                Instructions = instructions,
                ImageUrl = imageUrl,
                PortionsCount = portionsCount,
                PrepTime = TimeSpan.FromMinutes(prepTime),
                CookingTime = TimeSpan.FromMinutes(cookingTime),
                CategoryId = categoryId,
                ChefId = chefId,
            };

            this.data.Recipes.Add(validRecipe);
            this.data.SaveChanges();

            return validRecipe.Id;
        }

        public bool EditRecipe(int id, string title, string ingredients, string instructions, string imageUrl, int portionsCount, int prepTime, int cookingTime, int categoryId)
        {
            var recipeData = data.Recipes.Find(id);

            if (recipeData == null)
            {
                return false;
            }

            recipeData.Title = title;
            recipeData.Ingredients = ingredients;
            recipeData.Instructions = instructions;
            recipeData.ImageUrl = imageUrl;
            recipeData.PortionsCount = portionsCount;
            recipeData.PrepTime = TimeSpan.FromMinutes(prepTime);
            recipeData.CookingTime = TimeSpan.FromMinutes(cookingTime);
            recipeData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public bool IsByChef(int recipeId, int chefId)
            => this.data
            .Recipes
            .Any(r => r.Id == recipeId && r.ChefId == chefId);

        public IEnumerable<RecipeCategoryViewModel> GetRecipeCategories()
      => this.data
      .Categories
      .Select(c => new RecipeCategoryViewModel
      {
          Id = c.Id,
          Name = c.Name,
      })
      .ToArray();

        private IEnumerable<RecipeServiceModel> GetRecipes(IQueryable<Recipe> recipeQuery)
          => recipeQuery
            .Select(r => new RecipeServiceModel
          {
              Id = r.Id,
              Title = r.Title,
              ImageUrl = r.ImageUrl,
              PrepTime = r.PrepTime,
              CookingTime = r.CookingTime,
              PortionsCount = r.PortionsCount,
              CategoryName = r.Category.Name,
          })
            .ToList();

       
    }
}
