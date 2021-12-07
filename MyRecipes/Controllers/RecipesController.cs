namespace MyRecipes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data;
    using MyRecipes.Data.Models;
    using MyRecipes.Models.Recipes;

    using FileSystem = System.IO.File;
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
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                PortionsCount = recipe.PortionsCount,
                PrepTime = TimeSpan.FromMinutes(recipe.PrepTime),
                CookingTime = TimeSpan.FromMinutes(recipe.CookingTime),
                CategoryId = recipe.CategoryId,
            };

            this.data.Recipes.Add(validRecipe);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllRecipesViewModel query)
        {
            var recipesQuery = this.data.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                recipesQuery = recipesQuery.Where(r => r.Category.Name == query.Category);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                recipesQuery = recipesQuery
                    .Where(r => r.Title.ToLower().Contains(query.SearchTerm.ToLower())
                    || r.Category.Name.ToLower().Contains(query.SearchTerm.ToLower())
                    || r.Instructions.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            var totalRecipes = recipesQuery.Count();

            var recipes = recipesQuery
                .OrderByDescending(r => r.Id)
                .Skip((query.CurrentPage -1) * AllRecipesViewModel.RecipesPerPage)
                .Take(AllRecipesViewModel.RecipesPerPage)
                .Select(r => new RecipeListingViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl,
                    PrepTime = r.PrepTime,
                    CookingTime = r.CookingTime,
                    PortionsCount = r.PortionsCount,
                    Category = r.Category.Name,
                })
                .ToList();

            var recipeCategories = this.data
                .Recipes
                .Select(r => r.Category.Name)
                .Distinct()
                .ToArray();

            query.Categories = recipeCategories;
            query.Recipes = recipes;
            query.TotalRecipes = totalRecipes;

            return View(query);
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
