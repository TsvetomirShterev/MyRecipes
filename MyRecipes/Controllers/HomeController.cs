namespace MyRecipes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using MyRecipes.Models.Home;
    using MyRecipes.Services.Statistics;

    using static WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache;

        public HomeController(IStatisticsService statistics, IMapper mapper, IMemoryCache cache)
        {
            this.statistics = statistics;
            this.mapper = mapper;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var latestRecipes = this.cache.Get<List<RecipeIndexViewModel>>(LatestRecipesCacheKey);

            if (latestRecipes == null)
            {
                latestRecipes = this.statistics.Latest().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(LatestRecipesCacheKey, latestRecipes, cacheOptions);
            }


            var statistics = this.statistics.GetStatistics();

            var recipeViewModel = this.mapper.Map<IndexViewModel>(statistics);

            recipeViewModel.Recipes = latestRecipes;

            return View(recipeViewModel);
        }

        public IActionResult Error() => View();
    }
}
