namespace MyRecipes.Controllers.Api
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Models.Api.Statistics;
    using MyRecipes.Services.Statistics;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsApiController(IStatisticsService statistics)
            => this.statistics = statistics;

        [HttpGet]
        public StatisticsResponseModel GetStatistics() 
            => statistics.GetStatistics();
    }
}
