using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class RoomQualityRepositoryPgs:IRepository<RoomQuality>
{
    private HotelDbContext db;
    public RoomQualityRepositoryPgs(HotelDbContext dbcontext)
    {
        this.db=dbcontext;
    }

    public List<RoomQuality> GetList()
    {
        return db.RoomQualities.ToList();
    }
}