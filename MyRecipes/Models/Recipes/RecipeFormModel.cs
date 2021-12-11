namespace MyRecipes.Models.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyRecipes.Data.Models;
    using MyRecipes.Services.Recipes;

    using static Data.DataConstants.RecipeConstants;
    public class RecipeFormModel : IRecipeModel
    {

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; init; }

        [Required]
        [StringLength(IngredientsMaxLength, MinimumLength = IngredientsMinLength)]
        public string Ingredients { get; init; }

        [Required]
        [StringLength(InstructionMaxLength, MinimumLength = INstructionsMinLength)]
        public string Instructions { get; init; }

        [Required]
        public string ImageUrl { get; set; }

        [Range(0, 24 * 60)]
        [Display(Name = "Preparation time (in minutes)")]
        public int PrepTime { get; set; }

        [Range(10, 24 * 60)]
        [Display(Name = "Cooking time (in minutes)")]
        public int CookingTime { get; set; }

        [Range(1, 100)]
        public int PortionsCount { get; set; }

        [Display(Name = nameof(Category))]
        public int CategoryId { get; init; }

        public string CategoryName { get; set; }

        public IEnumerable<RecipeCategoryViewModel> Categories { get; set; }

    }
}
