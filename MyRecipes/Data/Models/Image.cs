namespace MyRecipes.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        public int Id { get; init; }

        [Required]
        public string ImageUrl { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}