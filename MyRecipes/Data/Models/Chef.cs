namespace MyRecipes.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using static DataConstants.ChefConstants;
    public class Chef
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }


        [Required]
        public string UserId { get; set; }

        public IEnumerable<Recipe> Recipes { get; init; } = new List<Recipe>();
    }
}
