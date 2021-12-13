namespace MyRecipes.Test.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyRecipes.Data.Models;

    public static class Recipes
    {
        public static IEnumerable<Recipe> TenPublicRecipes
          => Enumerable.Range(0, 10).Select(i => new Recipe
          {
              IsPublic = true,
          });
    }
}
