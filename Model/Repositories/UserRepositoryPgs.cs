using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class UserRepositoryPgs:IRepository<User>
{
    private HotelDbContext db;
    public UserRepositoryPgs(HotelDbContext dbcontext)
    {
        this.db=dbcontext;
    }

    public List<User> GetList()
    {
        return db.Users.ToList();
    }
}