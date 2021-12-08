namespace MyRecipes.Services.Recipes
{
    using System.Collections.Generic;

    public interface IRecipeService
    {
        RecipeQueryServiceModel All(string category,
            string searchTerm,
            int currentPage,
            int recipesPerPage);

        IEnumerable<string> AllRecipeCategories();
    }
}
