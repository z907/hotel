using Model.Context;
using Ninject;

namespace Hotel.ViewModel;

public class GlobalKernel
{
    public static IKernel Kernel;
    
   static GlobalKernel()
    {
        Kernel=new StandardKernel();
        Kernel.Bind<HotelDbContext>().ToSelf().InSingletonScope(); 
    }
}