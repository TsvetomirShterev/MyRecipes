namespace MyRecipes.Test.Routing
{
    using MyRecipes.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
          => MyRouting
          .Configuration()
          .ShouldMap("/")
          .To<HomeController>(r => r.Index());

        [Fact]
        public void ErroRouteShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Home/Error")
            .To<HomeController>(r => r.Error());
    }
}
