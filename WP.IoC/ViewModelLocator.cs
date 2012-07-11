using WP.IoC.NinjectSamples;

namespace WP.IoC
{
    public class ViewModelLocator
    {
        public NinjectSampleViewModel NinjectSampleVm
        {
            get { return IoCContainter.Get<NinjectSampleViewModel>(); }
        }
    }
}
