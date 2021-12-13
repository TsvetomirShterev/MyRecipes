namespace MyRecipes.Test.Routing
{
    using MyRecipes.Controllers;
    using MyRecipes.Models.Chefs;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ChefsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Chefs/Become")
            .To<ChefsController>(c => c.Become());

        [Fact]
        public void PostBecomeShouldBeMapped()
           => MyRouting
           .Configuration()
           .ShouldMap(request => request
                     .WithPath("/Chefs/Become")
                     .WithMethod(HttpMethod.Post))
            .To<ChefsController>(c => c.Become(With.Any<BecomeChefFormModel>()));
    }
}
