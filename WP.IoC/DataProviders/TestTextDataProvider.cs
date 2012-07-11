namespace WP.IoC.DataProviders
{
    public class TestTextDataProvider : ITextDataProvider
    {
        public string GetText()
        {
            return "test text";
        }
    }
}
