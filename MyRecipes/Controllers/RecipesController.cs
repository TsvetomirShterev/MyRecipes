namespace MyRecipes.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Infrastrucutre.Extentions;
    using MyRecipes.Models.Recipes;
    using MyRecipes.Services.Chefs;
    using MyRecipes.Services.Recipes;


    public class RecipesController : Controller
    {

        private readonly IRecipeService recipes;
        private readonly IChefService chefs;

        public RecipesController(IRecipeService recipes, IChefService chefs)
        {
            this.recipes = recipes;
            this.chefs = chefs;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.chefs.UserIsChef(this.User.GetId()))
            {
                return RedirectToAction(nameof(ChefsController.Become), "Chefs");
            }

            return View(new RecipeFormModel
            {
                Categories = this.recipes.GetRecipeCategories(),
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(RecipeFormModel recipe)
        {
            var chefId = this.chefs.GetIdByUser(this.User.GetId());

            if (chefId == 0)
            {
                return RedirectToAction("Become", "Chefs");
            }

            if (!this.recipes.CategoryExists(recipe.CategoryId))
            {
                this.ModelState.AddModelError(nameof(recipe.CategoryId), "This category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                recipe.Categories = this.recipes.GetRecipeCategories();

                return View(recipe);
            }

            this.recipes.CreateRecipe(
                recipe.Title,
                recipe.Ingredients,
                recipe.Instructions,
                recipe.ImageUrl,
                recipe.PortionsCount,
                recipe.PrepTime,
                recipe.CookingTime,
                recipe.CategoryId,
                chefId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.chefs.UserIsChef(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(ChefsController.Become), "Chefs");
            }

            var recipe = this.recipes.Details(id);

            if (recipe.UserId != userId && !User.IsAdmin())
            {
                return BadRequest();
            }

            return View(new RecipeFormModel
            {
                Title = recipe.Title,
                ImageUrl = recipe.ImageUrl,
                Instructions = recipe.Instructions,
                Ingredients = recipe.Ingredients,
                PrepTime = (int)recipe.PrepTime.TotalMinutes,
                CookingTime = (int)recipe.CookingTime.TotalMinutes,
                PortionsCount = recipe.PortionsCount,
                CategoryId = recipe.CategoryId,
                Categories = this.recipes.GetRecipeCategories(),
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, RecipeFormModel recipe)
        {
            var chefId = this.chefs.GetIdByUser(this.User.GetId());

            if (chefId == 0 && !User.IsAdmin())
            {
                return RedirectToAction("Become", "Chefs");
            }

            if (!this.recipes.CategoryExists(recipe.CategoryId))
            {
                this.ModelState.AddModelError(nameof(recipe.CategoryId), "This category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                recipe.Categories = this.recipes.GetRecipeCategories();

                return View(recipe);
            }

            if (!this.recipes.IsByChef(id, chefId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.recipes.EditRecipe(
                 id,
                 recipe.Title,
                 recipe.Ingredients,
                 recipe.Instructions,
                 recipe.ImageUrl,
                 recipe.PortionsCount,
                 recipe.PrepTime,
                 recipe.CookingTime,
                 recipe.CategoryId
                 );

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

        [Authorize]
        public IActionResult MyRecipes()
        {
            var myRecipes = this.recipes.ByUser(this.User.GetId());

            return View(myRecipes);
        }
    }
}
