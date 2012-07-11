namespace WP.IoC.DataProviders
{
    public class ProductionDataProvider : ITextDataProvider
    {
        public string GetText()
        {
            return "production text";
        }
    }
}
