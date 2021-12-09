namespace MyRecipes.Services.Chefs
{
    using System.Linq;

    using MyRecipes.Data;
    using MyRecipes.Data.Models;

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

        public void Create(string chefName, string userId)
        {
            var validChef = new Chef
            {
                Name = chefName,
                UserId = userId,
            };

            this.data.Chefs.Add(validChef);
            this.data.SaveChanges();
        }
    }
}
