namespace MyRecipes.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.RecipeConstants;
    public class Recipe
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public TimeSpan PrepTime { get; set; }
    
        public TimeSpan CookingTime { get; set; }

        public int PortionsCount { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
