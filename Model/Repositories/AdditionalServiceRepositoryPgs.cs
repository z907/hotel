using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class AdditionalServiceRepositoryPgs:IRepository<AdditionalService>
{
    private HotelDbContext db;
    public AdditionalServiceRepositoryPgs(HotelDbContext dbcontext)
    {
        this.db=dbcontext;
    }

    public List<AdditionalService> GetList()
    {
        return db.AdditionalServices.ToList();
    }
}