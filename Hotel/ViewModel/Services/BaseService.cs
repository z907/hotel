using Model.Context;
using Ninject;
using Hotel.ViewModel;

namespace ViewModel.Services;

public class BaseService
{
    public HotelDbContext db;
    public BaseService()
    {
        db = GlobalKernel.Kernel.Get<HotelDbContext>();
    }
}