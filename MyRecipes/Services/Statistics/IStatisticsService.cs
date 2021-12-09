namespace MyRecipes.Services.Statistics
{
    using System.Collections.Generic;

    using MyRecipes.Models.Api.Statistics;
    using MyRecipes.Models.Home;

    public interface IStatisticsService
    {
        StatisticsResponseModel GetStatistics();

        IEnumerable<RecipeIndexViewModel> Latest();

    }
}
