namespace MyRecipes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data;
    using MyRecipes.Data.Models;
    using MyRecipes.Infrastrucutre.Extentions;
    using MyRecipes.Models.Recipes;
    using MyRecipes.Services.Recipes;


    public class RecipesController : Controller
    {

        private readonly RecipeDbContext data;
        private readonly IRecipeService recipes;

        public RecipesController(RecipeDbContext data, IRecipeService recipes)
        {
            this.data = data;
            this.recipes = recipes;
        }

        [Authorize]
        public IActionResult Add()
        {
            var chefId = this.data
               .Chefs
               .Where(c => c.UserId == this.User.GetId())
               .Select(c => c.Id)
               .FirstOrDefault();

            if (chefId == 0)
            {
                return RedirectToAction(nameof(ChefsController.Become), "Chefs");
            }

            return View(new AddRecipeFormModel
            {
                Categories = this.GetRecipeCategories(),
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddRecipeFormModel recipe)
        {
            var chefId = this.data
                .Chefs
                .Where(c => c.UserId == this.User.GetId())
                .Select(c => c.Id)
                .FirstOrDefault();

            if (chefId == 0)
            {
                return RedirectToAction("Become", "Chefs");
            }

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
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                PortionsCount = recipe.PortionsCount,
                PrepTime = TimeSpan.FromMinutes(recipe.PrepTime),
                CookingTime = TimeSpan.FromMinutes(recipe.CookingTime),
                CategoryId = recipe.CategoryId,
                ChefId = chefId,
            };

            this.data.Recipes.Add(validRecipe);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllRecipesViewModel query)
        {
            var recipes = this.recipes.All(
                query.Category,
                query.SearchTerm,
                query.CurrentPage,
                AllRecipesViewModel.RecipesPerPage);

            var recipeCategories = this.recipes.AllRecipeCategories();

            query.Categories = recipeCategories;
            query.TotalRecipes = recipes.TotalRecipes;
            query.Recipes = recipes.Recipes;

            return View(query);
        }

        private bool UserIsChef()
            => this.data
                .Chefs
                .Any(c => c.UserId == this.User.GetId());

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
