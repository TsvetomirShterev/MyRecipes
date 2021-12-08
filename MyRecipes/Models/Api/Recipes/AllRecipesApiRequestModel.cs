namespace MyRecipes.Models.Api.Recipes
{
    public class AllRecipesApiRequestModel
    {

        public string Category { get; init; }

        public string SearchTerm { get; set; }

        public int CurrentPage { get; init; } = 1;

        public int RecipesPerPage { get; init; } = 10;

        public int TotalRecipes { get; set; }
    }
}
