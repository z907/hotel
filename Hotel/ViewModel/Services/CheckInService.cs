using Hotel.ViewModel.VmEntities;
using Microsoft.EntityFrameworkCore;

namespace ViewModel.Services;

public class CheckInService:BaseService
{
    private PayService payServ = new PayService();
    public List<CheckIn> GetAllCheckIns()
    {
        db.ReservationStatuses.Load();
        db.RoomAttributes.Load();
        db.RoomCapacities.Load();
        db.RoomQualities.Load();
        db.RoomViewTypes.Load();
        db.Customers.Load();
        db.Rooms.Load();
        
        return db.Reservations.Where(r => r.Status.Status == "Не оплачено" || r.Status.Status == "Оплачено")
            .ToList().Select(r=>new CheckIn
            {
                Id=r.Id,CustomerId = r.CustomerId,RoomId = r.RoomId,StatusId = r.StatusId,
                Status = r.Status.Status,RoomNum = r.Room.Number,CustomerSurname = r.Customer.Surname,
                TotalCost = r.TotalCost,RoomCapacity = r.RoomAttributes.Capacity.Value,
                RoomViewType = r.RoomAttributes.View.Name,RoomQuality = r.RoomAttributes.Quality.Name,
                StartDate = r.StartDate,EndDate = r.EndDate,ToPay = payServ.CountLeftover(r.Id)
            }).ToList();
    }

    public CheckIn GetCheckInById(int chId)
    {
        db.ReservationStatuses.Load();
        db.RoomAttributes.Load();
        db.RoomCapacities.Load();
        db.RoomQualities.Load();
        db.RoomViewTypes.Load();
        db.Customers.Load();
        db.Rooms.Load();
        
        var r=db.Reservations.Find(chId);
        return new CheckIn
        {
            Id = r.Id, CustomerId = r.CustomerId, RoomId = r.RoomId, StatusId = r.StatusId,
            Status = r.Status.Status, RoomNum = r.Room.Number, CustomerSurname = r.Customer.Surname,
            TotalCost = r.TotalCost, RoomCapacity = r.RoomAttributes.Capacity.Value,
            RoomViewType = r.RoomAttributes.View.Name, RoomQuality = r.RoomAttributes.Quality.Name,
            StartDate = r.StartDate, EndDate = r.EndDate, ToPay = payServ.CountLeftover(r.Id)
        };
    }

    public void FinishCheckIn(int chId)
    {
        db.Reservations.Find(chId).StatusId = db.ReservationStatuses.First(s => s.Status == "Завершено").Id;
        db.SaveChanges();
    }
    
}