namespace MyRecipes.Services.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyRecipes.Data;
    using MyRecipes.Data.Models;
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
            string category = null,
           string searchTerm = null,
           int currentPage = 1,
           int recipesPerPage = int.MaxValue,
           bool publicOnly = true)
        {
            var recipesQuery = this.data
                .Recipes
                .Where(r => !publicOnly || r.IsPublic);

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
                .ProjectTo<RecipeServiceModel>(this.mapper.ConfigurationProvider)
                .Skip((currentPage - 1) * recipesPerPage)
                .Take(recipesPerPage)
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

        public IEnumerable<RecipeServiceModel> ByUser(string userId)
            => this.data.Recipes
                 .Where(c => c.Chef.UserId == userId)
                 .ProjectTo<RecipeServiceModel>(this.mapper.ConfigurationProvider)
                 .ToList();


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

        public int CreateRecipe(
            string title,
            string ingredients,
            string instructions,
            string imageUrl,
            int portionsCount,
            int prepTime,
            int cookingTime,
            int categoryId,
            int chefId)
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
                IsPublic = false,
            };

            this.data.Recipes.Add(validRecipe);
            this.data.SaveChanges();

            return validRecipe.Id;
        }

        public int EditRecipe(
            int id,
            string title,
            string ingredients,
            string instructions,
            string imageUrl,
            int portionsCount,
            int prepTime,
            int cookingTime,
            int categoryId,
            bool isPublic)
        {
            var recipeData = data.Recipes.Find(id);

            if (recipeData == null)
            {
                return 0;
            }

            recipeData.Title = title;
            recipeData.Ingredients = ingredients;
            recipeData.Instructions = instructions;
            recipeData.ImageUrl = imageUrl;
            recipeData.PortionsCount = portionsCount;
            recipeData.PrepTime = TimeSpan.FromMinutes(prepTime);
            recipeData.CookingTime = TimeSpan.FromMinutes(cookingTime);
            recipeData.CategoryId = categoryId;
            recipeData.IsPublic = isPublic;

            this.data.SaveChanges();

            return recipeData.Id;
        }

        public bool IsByChef(int recipeId, int chefId)
            => this.data
            .Recipes
            .Any(r => r.Id == recipeId && r.ChefId == chefId);

        public IEnumerable<RecipeCategoryViewModel> GetRecipeCategories()
           => this.data
           .Categories
           .ProjectTo<RecipeCategoryViewModel>(this.mapper.ConfigurationProvider)
           .ToArray();

        public void ChangeVisibility(int recipeId)
        {
            var recipe = this.data.Recipes.Find(recipeId);

            recipe.IsPublic = !recipe.IsPublic;

            this.data.SaveChanges();
        }
    }
}
