namespace MyRecipes.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data;
    using MyRecipes.Data.Models;
    using MyRecipes.Models.Recipes;
    using System.Collections.Generic;
    using System.Linq;

    public class RecipesController : Controller
    {

        private readonly RecipeDbContext data;

        public RecipesController(RecipeDbContext data)
            => this.data = data;

        //[Authorize]
        public IActionResult Add()
        {

            return View(new AddRecipeFormModel
            {
                Categories = this.GetRecipeCategroeis(),
            });
        }

        [HttpPost]
        public IActionResult Add(AddRecipeFormModel recipe)
        {
            if (!this.data.Categories.Any(c => c.Id == recipe.CategoryId))
            {
                this.ModelState.AddModelError(nameof(recipe.CategoryId), "This category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                recipe.Categories = this.GetRecipeCategroeis();

                return View(recipe);
            }

            var validRecipe = new Recipe
            {
                Title = recipe.Title,
                Description = recipe.Description,
                ImageUrl = recipe.ImageUrl,
                CategoryId = recipe.CategoryId,
            };

            this.data.Recipes.Add(validRecipe);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        private IEnumerable<RecipeCategoryViewModel> GetRecipeCategroeis()
            => this.data
            .Categories
            .Select(c => new RecipeCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToArray();


    }
}
