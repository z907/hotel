using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class RoomAttributeRepositoryPgs:IRepository<RoomAttribute>
{
    private HotelDbContext db;
    public RoomAttributeRepositoryPgs(HotelDbContext dbcontext)
    {
        this.db=dbcontext;
    }

    public List<RoomAttribute> GetList()
    {
        return db.RoomAttributes.ToList();
    }
}