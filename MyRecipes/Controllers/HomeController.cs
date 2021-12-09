namespace MyRecipes.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data;
    using MyRecipes.Models;
    using MyRecipes.Models.Home;
    using MyRecipes.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly RecipeDbContext data;
        private readonly IStatisticsService statistics;
        private readonly IMapper mapper;

        public HomeController(RecipeDbContext data, IStatisticsService statistics, IMapper mapper)
        {
            this.data = data;
            this.statistics = statistics;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {

            var recipes = this.data
               .Recipes
               .OrderByDescending(r => r.Id)
               .ProjectTo<RecipeIndexViewModel>(this.mapper.ConfigurationProvider)
               .Take(3)
               .ToList();

            var statistics = this.statistics.GetStatistics();

            var recipeViewModel = this.mapper.Map<IndexViewModel>(statistics);

            recipeViewModel.Recipes = recipes;

            return View(recipeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
