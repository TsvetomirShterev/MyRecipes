namespace MyRecipes.Services.Recipes
{
    public class RecipesInstructionsServiceModel : RecipeServiceModel
    {
        public string Instructions { get; init; }

        public string Ingredients { get; init; }

        public int ChefId { get; init; }

        public string ChefName { get; init; }

        public string UserId { get; init; }

        public int CategoryId { get; init; }
    }
}
