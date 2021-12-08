namespace MyRecipes.Services.Statistics
{
    using MyRecipes.Models.Api.Statistics;

    public interface IStatisticsService
    {
        StatisticsResponseModel GetStatistics();
    }
}
