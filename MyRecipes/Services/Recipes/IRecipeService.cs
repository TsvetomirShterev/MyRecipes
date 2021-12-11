namespace MyRecipes.Services.Recipes
{
    using System.Collections.Generic;

    using MyRecipes.Models.Recipes;

    public interface IRecipeService
    {
        int CreateRecipe(
              string title,
              string ingredients,
              string instructions,
              string imageUrl,
              int portionsCount,
              int prepTime,
              int cookingTime,
              int categoryId,
              int chefId);

        RecipeQueryServiceModel All(
           string category = null,
           string searchTerm = null,
           int currentPage = 1,
           int recipesPerPage = int.MaxValue,
           bool publicOnly = true);

        int EditRecipe(
            int id,
            string title,
            string ingredients,
            string instructions,
            string imageUrl,
            int portionsCount,
            int prepTime,
            int cookingTime,
            int categoryId, 
            bool isPublic);

        IEnumerable<RecipeCategoryViewModel> GetRecipeCategories();

        IEnumerable<RecipeServiceModel> ByUser(string userId);

        bool IsByChef(int recipeId, int chefId);

        IEnumerable<string> AllRecipeCategories();

        RecipesInstructionsServiceModel Details(int recipeId);

        bool CategoryExists(int categoryId);

        void ChangeVisibility(int idrecipeId);
    }
}
