namespace WP.Common
{
    public sealed class Constants
    {
        public sealed class Images
        {
            public sealed class Paths
            {
                public const string LogoPath = "sdsadasdsad";
            }
        }
    }

    public class Test
    {
        public Test()
        {
            var something = Constants.Images.Paths.LogoPath;
        }
    }
}
