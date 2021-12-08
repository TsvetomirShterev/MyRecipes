namespace MyRecipes.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Models.Api.Recipes;
    using MyRecipes.Services.Recipes;

    [ApiController]
    [Route("api/recipes")]
    public class RecipesApiController : ControllerBase
    {
        private readonly IRecipeService recipes;

        public RecipesApiController(IRecipeService recipes)
            => this.recipes = recipes;

        [HttpGet]
        public RecipeQueryServiceModel AllRecipes([FromQuery] AllRecipesApiRequestModel query)
            => this.recipes.All(
                query.Category, 
                query.SearchTerm, 
                query.CurrentPage,
                query.RecipesPerPage);
    }
}
