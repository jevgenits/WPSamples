using WP.IoC.DataProviders;
using Ninject;

namespace WP.IoC.NinjectSamples
{
public static class IoCContainter 
{
    private static readonly IKernel Kernel = new StandardKernel();

    public static void Initialize()
    {
        #if DEBUG
           
        Kernel.Bind<ITextDataProvider>().To<TestTextDataProvider>();
            
        #else
            
        Kernel.Bind<ITextDataProvider>().To<ProductionDataProvider>();
            
        #endif
    }

    public static T Get<T>()
    {
        return Kernel.Get<T>();
    }
}
}
