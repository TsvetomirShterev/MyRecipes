namespace MyRecipes.Data
{
    public class DataConstants
    {

        public class CuisineConstants
        {
            public const int CuisineMaxLength = 30;
        }

        public class CategoryConstants
        {
            public const int CategoryNameMaxLength = 30;
        }
     
        public class RecipeConstants
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 30;

            public const int INstructionsMinLength = 10;
            public const int InstructionMaxLength = 255;

            public const int IngredientsMinLength = 5;
            public const int IngredientsMaxLength = 255;
        }

        public class UserConstants
        {
            public const int FullNameMaxLength = 50;
        }

        public class ChefConstants
        {
            public const int NameMinLength = 8;
            public const int NameMaxLength = 30;
        }
    }
}
