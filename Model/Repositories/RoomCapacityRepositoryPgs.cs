using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class RoomCapacityRepositoryPgs:IRepository<RoomCapacity>
{
    private HotelDbContext db;
    public RoomCapacityRepositoryPgs(HotelDbContext dbcontext)
    {
        this.db=dbcontext;
    }

    public List<RoomCapacity> GetList()
    {
        return db.RoomCapacities.ToList();
    }
}