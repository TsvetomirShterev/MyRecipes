namespace MyRecipes.Models.Recipes
{
    using MyRecipes.Services.Recipes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllRecipesViewModel
    {
        public const int RecipesPerPage = 6;

        public IEnumerable<RecipeServiceModel> Recipes { get; set; }

        public string Category { get; init; }

        public IEnumerable<string> Categories { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public int CurrentPage { get; init; } = 1;

        public int TotalRecipes { get; set; }
    }
}
