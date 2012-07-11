using WP.IoC.DataProviders;
using Ninject;

namespace WP.IoC.NinjectSamples
{
public class NinjectSampleViewModel
{
    [Inject]
    public ITextDataProvider TextDataProvider { get; set; }

    public string TextToDisplay
    {
        get { return TextDataProvider.GetText(); }
    }
}
}
