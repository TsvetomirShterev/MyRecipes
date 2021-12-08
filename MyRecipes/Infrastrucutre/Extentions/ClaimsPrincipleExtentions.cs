namespace MyRecipes.Infrastrucutre.Extentions
{
    using MyRecipes.Areas.Admin;
    using System.Security.Claims;

    public static class ClaimsPrincipleExtentions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdminConstants.AdministratorRoleName);
    }
}
