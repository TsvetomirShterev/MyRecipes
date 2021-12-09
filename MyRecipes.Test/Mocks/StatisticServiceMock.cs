namespace MyRecipes.Test.Mocks
{
    using Moq;
    using MyRecipes.Services.Statistics;

    public class StatisticServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                return null;
            }
        }
    }
}
