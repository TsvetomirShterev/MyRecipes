namespace MyRecipes.Models.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.CategoryConstants;
    public class RecipeCategoryViewModel
    {
        public int Id { get; init; }

        [StringLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
