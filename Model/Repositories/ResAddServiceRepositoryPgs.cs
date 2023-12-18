using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class ResAddServiceRepositoryPgs:IRepository<ResAddService>
{
    private HotelDbContext db;
    public ResAddServiceRepositoryPgs(HotelDbContext dbcontext)
    {
        this.db=dbcontext;
    }

    public List<ResAddService> GetList()
    {
        return db.ResAddServices.ToList();
    }
}