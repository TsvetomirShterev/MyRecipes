namespace MyRecipes.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data;
    using MyRecipes.Models;
    using MyRecipes.Models.Home;
    using MyRecipes.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly RecipeDbContext data;
        private readonly IStatisticsService statistics;

        public HomeController(RecipeDbContext data, IStatisticsService statistics)
        {
            this.data = data;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {

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

            var statistics = this.statistics.GetStatistics();

            return View(new IndexViewModel
            {
                TotalRecipes = statistics.TotalRecipes,
                TotalChefs = statistics.TotalChefs,
                TotalUsers = statistics.TotalUsers,
                Recipes = recipes,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
