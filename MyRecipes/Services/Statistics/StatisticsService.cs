namespace MyRecipes.Services.Statistics
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MyRecipes.Data;
    using MyRecipes.Models.Api.Statistics;
    using MyRecipes.Models.Home;

    public class StatisticsService : IStatisticsService
    {
        private readonly RecipeDbContext data;
        private readonly IMapper mapper;

        public StatisticsService(RecipeDbContext data, IMapper mapper = null)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public StatisticsResponseModel GetStatistics()
        {
            var totalRecipes = this.data.Recipes.Count(r => r.IsPublic);
            var totalChefs = this.data.Chefs.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsResponseModel
            {
                TotalRecipes = totalRecipes,
                TotalChefs = totalChefs,
                TotalUsers = totalUsers,
            };
        }

        public IEnumerable<RecipeIndexViewModel> Latest()
         => this.data
              .Recipes
              .Where(r => r.IsPublic)
              .OrderByDescending(r => r.Id)
              .ProjectTo<RecipeIndexViewModel>(this.mapper.ConfigurationProvider)
              .Take(3)
              .ToList();
    }
}
