using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class RoomRepositoryPgs:IRepository<Room>
{
    private HotelDbContext db;
    public RoomRepositoryPgs(HotelDbContext dbcontext)
    {
        this.db=dbcontext;
    }

    public List<Room> GetList()
    {
        return db.Rooms.ToList();
    }
}