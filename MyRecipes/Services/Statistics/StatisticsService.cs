namespace MyRecipes.Services.Statistics
{
    using MyRecipes.Data;
    using MyRecipes.Models.Api.Statistics;
    using System;
    using System.Linq;

    public class StatisticsService : IStatisticsService
    {
        private readonly RecipeDbContext data;

        public StatisticsService(RecipeDbContext data)
            => this.data = data;

        public StatisticsResponseModel GetStatistics()
        {
            var totalRecipes = this.data.Recipes.Count();
            var totalChefs = this.data.Chefs.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsResponseModel
            {
                TotalRecipes = totalRecipes,
                TotalChefs = totalChefs,
                TotalUsers = totalUsers,
            };
        }
    }
}
