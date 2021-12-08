namespace MyRecipes.Services.Chefs
{
    public interface IChefService
    {
        public bool UserIsChef(string userId);

        public int GetIdByUser(string userId);
    }
}
