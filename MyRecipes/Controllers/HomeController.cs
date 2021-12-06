namespace MyRecipes.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data;
    using MyRecipes.Models;
    using MyRecipes.Models.Home;

    public class HomeController : Controller
    {
        private readonly RecipeDbContext data;

        public HomeController(RecipeDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var totalRecipes = this.data.Recipes.Count();

            var recipes = this.data
               .Recipes
               .OrderByDescending(r => r.Id)
               .Select(r => new RecipeIndexViewModel
               {
                   Id = r.Id,
                   Title = r.Title,
                   ImageUrl = r.ImageUrl,
                   PrepTime = r.PrepTime,
                   CookingTime = r.CookingTime,
                   PortionsCount = r.PortionsCount,
               })
               .Take(3)
               .ToList();

            return View(new IndexViewModel
            {
                TotalRecipes = totalRecipes,
                Recipes = recipes,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
