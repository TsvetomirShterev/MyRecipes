namespace MyRecipes.Test.Pipeline
{
    using MyRecipes.Controllers;
    using MyRecipes.Models.Home;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using static Data.Recipes;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
          => MyMvc
            .Pipeline()
            .ShouldMap("/")
            .To<HomeController>(r => r.Index())
            .Which(controller => controller
                  .WithData(TenPublicRecipes))
            .ShouldReturn()
            .View(view => view
                     .WithModelOfType<IndexViewModel>());


        [Fact]
        public void ErrorShouldReturnView()
          => MyMvc
            .Pipeline()
            .ShouldMap("/Home/Error")
            .To<HomeController>(r => r.Error())
            .Which()
            .ShouldReturn()
            .View();
    }
}
