namespace MyRecipes.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data;
    using MyRecipes.Data.Models;
    using MyRecipes.Infrastrucutre.Extentions;
    using MyRecipes.Models.Chefs;
    using MyRecipes.Services.Chefs;

    public class ChefsController : Controller
    {
        private readonly RecipeDbContext data;
        private readonly IChefService chefs;

        public ChefsController(RecipeDbContext data, IChefService chefs)
        {
            this.data = data;
            this.chefs = chefs;
        }

        [Authorize]
        public IActionResult Become()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Become(BecomeChefFormModel chef)
        {
            var userId = this.User.GetId();

            var userIsAlreadyChef = this.chefs.UserIsChef(userId);

            if (userIsAlreadyChef)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(chef);
            }

            this.chefs.Create(chef.Name, userId);

            return RedirectToAction("All", "Recipes");
        }
    }
}
