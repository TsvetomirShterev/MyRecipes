namespace MyRecipes.Models.Home
{
    using System;


    public class RecipeIndexViewModel
    {
        public int Id { get; set; }

        public string Title { get; init; }

        public string ImageUrl { get; set; }

        public TimeSpan PrepTime { get; set; }

        public TimeSpan CookingTime { get; set; }

        public int PortionsCount { get; set; }
    }
}
