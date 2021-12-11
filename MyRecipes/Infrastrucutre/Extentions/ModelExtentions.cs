namespace MyRecipes.Infrastrucutre.Extentions
{
    using MyRecipes.Services.Recipes;

    public static class ModelExtentions
    {
        public static string ToFriendlyUrl(this IRecipeModel recipe) 
            => recipe.Title + "-" + recipe.CategoryName;
    }
}
