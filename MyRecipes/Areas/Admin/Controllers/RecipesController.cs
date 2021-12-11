namespace MyRecipes.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Services.Recipes;

    public class RecipesController : AdminController
    {
        private readonly IRecipeService recipes;

        public RecipesController(IRecipeService recipes) 
            => this.recipes = recipes;

        public IActionResult All()
        {
            var recipes = this.recipes.All(publicOnly: false).Recipes;

            return View(recipes);
        }

        public IActionResult ChangeVisibility(int id )
        {
            this.recipes.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
