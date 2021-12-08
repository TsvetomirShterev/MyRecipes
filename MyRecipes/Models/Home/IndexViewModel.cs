namespace MyRecipes.Models.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalChefs { get; init; }

        public int TotalUsers { get; init; }

        public int TotalRecipes { get; init; }

        public IEnumerable<RecipeIndexViewModel> Recipes { get; init; }
    }
}
