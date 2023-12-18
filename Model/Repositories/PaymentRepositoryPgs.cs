using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class PaymentRepositoryPgs:IRepository<Payment>
{
    private HotelDbContext db;
    public PaymentRepositoryPgs(HotelDbContext dbcontext)
    {
        this.db=dbcontext;
    }

    public List<Payment> GetList()
    {
        return db.Payments.ToList();
    }
}