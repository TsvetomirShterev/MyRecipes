namespace MyRecipes.Test.Controllers
{
    using MyRecipes.Controllers;
    using MyRecipes.Models.Home;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using static Data.Recipes;
    using static WebConstants.Cache;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexActionShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                .Instance(instance => instance
                          .WithData(TenPublicRecipes))
                .Calling(r => r.Index())
                .ShouldHave()
                .MemoryCache(cache => cache
                      .ContainingEntryWithKey(LatestRecipesCacheKey))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                .WithModelOfType<IndexViewModel>());


        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
            .Instance()
            .Calling(r => r.Error())
            .ShouldReturn()
            .View();
    }
}
