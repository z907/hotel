using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class ReservationRepositoryPgs:IRepository<Reservation>
{
    private HotelDbContext db;
    public ReservationRepositoryPgs(HotelDbContext dbcontext)
    {
        this.db=dbcontext;
    }

    public List<Reservation> GetList()
    {
        return db.Reservations.ToList();
    }
}