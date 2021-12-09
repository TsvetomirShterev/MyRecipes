namespace MyRecipes.Test.Api
{
    using MyRecipes.Controllers.Api;
    using MyRecipes.Test.Mocks;
    using Xunit;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            //Arrange
            var statisticsController = new StatisticsApiController(StatisticServiceMock.Instance);


            //Act


            //Assert
        }
    }
}
