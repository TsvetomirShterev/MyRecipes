namespace MyRecipes.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data;
    using MyRecipes.Data.Models;
    using MyRecipes.Infrastrucutre.Extentions;
    using MyRecipes.Models.Chefs;
    using System.Linq;

    public class ChefsController : Controller
    {
        private readonly RecipeDbContext data;

        public ChefsController(RecipeDbContext data)
            => this.data = data;

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

            var userIsAlreadyChef = this.data
                .Chefs
                .Any(c => c.UserId == userId);

            if (userIsAlreadyChef)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(chef);
            }

            var validChef = new Chef
            {
                Name = chef.Name,
                UserId = userId,
            };

            this.data.Chefs.Add(validChef);
            this.data.SaveChanges();

            return RedirectToAction("All", "Recipes");
        }
    }
}
