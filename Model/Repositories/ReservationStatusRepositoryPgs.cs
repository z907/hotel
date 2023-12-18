using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class ReservationStatusRepositoryPgs:IRepository<ReservationStatus>
{
private HotelDbContext db;
public ReservationStatusRepositoryPgs(HotelDbContext dbcontext)
{
    this.db=dbcontext;
}

public List<ReservationStatus> GetList()
{
    return db.ReservationStatuses.ToList();
}
}