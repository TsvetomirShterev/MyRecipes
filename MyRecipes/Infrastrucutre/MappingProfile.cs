namespace MyRecipes.Infrastrucutre
{
    using AutoMapper;
    using MyRecipes.Data.Models;
    using MyRecipes.Models.Api.Statistics;
    using MyRecipes.Models.Home;
    using MyRecipes.Models.Recipes;
    using MyRecipes.Services.Recipes;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Recipe, RecipeIndexViewModel>();

            this.CreateMap<StatisticsResponseModel, IndexViewModel>();

            this.CreateMap<Recipe, RecipesInstructionsServiceModel>()
                .ForMember(r => r.CategoryName, cfg => cfg.MapFrom(r => r.Category.Name))
                .ForMember(r => r.ChefName, cfg => cfg.MapFrom(r => r.Chef.Name))
                .ForMember(r => r.UserId, cfg => cfg.MapFrom(r => r.Chef.UserId));

            this.CreateMap<RecipesInstructionsServiceModel, RecipeFormModel>()
                .ForMember(r => r.CookingTime, f => f.Ignore())
                .ForMember(x => x.PrepTime, y => y.Ignore());

            this.CreateMap<Category, RecipeCategoryViewModel>();

            this.CreateMap<Recipe, RecipeServiceModel>()
                .ForMember(r => r.CategoryName, cfg => cfg.MapFrom(r => r.Category.Name));
        }
    }
}


