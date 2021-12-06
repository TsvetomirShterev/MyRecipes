namespace MyRecipes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data;
    using MyRecipes.Data.Models;
    using MyRecipes.Models.Recipes;

    using FileSystem = System.IO.File;
    public class RecipesController : Controller
    {

        private readonly RecipeDbContext data;

        public RecipesController(RecipeDbContext data)
        {
            this.data = data;
        }

        //[Authorize]
        public IActionResult Add()
        {

            return View(new AddRecipeFormModel
            {
                Categories = this.GetRecipeCategories(),
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
                recipe.Categories = this.GetRecipeCategories();

                return View(recipe);
            }

            var validRecipe = new Recipe
            {
                Title = recipe.Title,
                Ingredients = recipe.Ingredients,
                Description = recipe.Description,
                ImageUrl = recipe.ImageUrl,
                PortionsCount = recipe.PortionsCount,
                PrepTime = TimeSpan.FromMinutes(recipe.PrepTime),
                CookingTime = TimeSpan.FromMinutes(recipe.CookingTime),
                CategoryId = recipe.CategoryId,
            };

           
            
            this.data.Recipes.Add(validRecipe);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        private IEnumerable<RecipeCategoryViewModel> GetRecipeCategories()
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
