using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class UserRoleRepositoryPgs:IRepository<UserRole>
{
    private HotelDbContext db;
    public UserRoleRepositoryPgs(HotelDbContext dbcontext)
    {
        this.db=dbcontext;
    }

    public List<UserRole> GetList()
    {
        return db.UserRoles.ToList();
    }
}