using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class CustomerRepositoryPgs:IRepository<Customer>
{
    private HotelDbContext db;
    public CustomerRepositoryPgs(HotelDbContext dbcontext)
    {
        this.db=dbcontext;
    }

    public List<Customer> GetList()
    {
        return db.Customers.ToList();
    }
}