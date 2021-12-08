namespace MyRecipes.Services.Chefs
{
    using System.Linq;

    using MyRecipes.Data;

    public class ChefService : IChefService
    {
        private readonly RecipeDbContext data;

        public ChefService(RecipeDbContext data)
            => this.data = data;

        public bool UserIsChef(string userId)
        {
            return this.data.Chefs.Any(c => c.UserId == userId);
        }

        public int GetIdByUser(string userId)
          =>  this.data
                .Chefs
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstOrDefault();
    }
}
