namespace MyRecipes.Test.Controllers
{
    using System.Linq;

    using MyRecipes.Controllers;
    using MyRecipes.Data.Models;
    using MyRecipes.Models.Chefs;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ChefsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsers()
            => MyController<ChefsController>
            .Instance()
            .Calling(c => c.Become())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests());

        [Fact]
        public void GetBecomeShouldReturnView()
            => MyController<ChefsController>
            .Instance()
            .Calling(c => c.Become())
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("Chef Name")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string chefName)
            => MyController<ChefsController>
            .Instance(controller => controller
                       .WithUser())
            .Calling(c => c.Become(new BecomeChefFormModel
            {
                Name = chefName,
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                             .RestrictingForHttpMethod(HttpMethod.Post)
                             .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                    .WithSet<Chef>(chefs => chefs
                    .Any(c => c.Name == chefName && c.UserId == TestUser.Identifier)));

    }
}
