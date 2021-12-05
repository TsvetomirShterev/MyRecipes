namespace MyRecipes.Data
{
    public class DataConstants
    {
        public class CategoryConstants
        {
            public const int CategoryNameMaxLength = 30;
        }


        public class IngridientConstants
        {
            public const int IngridientNameMaxLength = 20;
            public const int QuantityMaxLength = 15;
        }

        public class RecipeConstants
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 30;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 255;
        }

        public class UserConstants
        {
            public const int FullNameMaxLength = 50;
        }

        public class ChefConstants
        {
            public const int ChefNameMinLength = 8;
            public const int ChefNameMaxLength = 30;
        }
    }
}
