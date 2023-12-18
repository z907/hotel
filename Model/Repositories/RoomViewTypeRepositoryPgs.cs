using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class RoomViewTypeRepositoryPgs:IRepository<RoomViewType>
{
    private HotelDbContext db;
    public RoomViewTypeRepositoryPgs(HotelDbContext dbcontext)
    {
        this.db=dbcontext;
    }

    public List<RoomViewType> GetList()
    {
        return db.RoomViewTypes.ToList();
    }
}