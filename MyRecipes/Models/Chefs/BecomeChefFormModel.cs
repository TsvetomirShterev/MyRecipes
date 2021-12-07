namespace MyRecipes.Models.Chefs
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.ChefConstants;
    public class BecomeChefFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
