using System.Diagnostics;
using System.Runtime.CompilerServices;
using Hotel.VmEntities;
using Model.Entities;

using Microsoft.EntityFrameworkCore;
using ViewModel.VmEntities;

namespace ViewModel.Services;

public class ReservationService:BaseService
{
    
        public ReservationService() : base()
        {
            
        }

   
    public List<DisplayReservation> GetTodayReservation()
    {
        
        List<DisplayReservation> result=(from r in db.Reservations.
            Include(res=>res.Customer).
            Include(res=>res.RoomAttributes).
            Include(res=>res.RoomAttributes.View).
            Include(res=>res.RoomAttributes.Quality).
            Include(res=>res.RoomAttributes.Capacity)
            select new DisplayReservation{Id=r.Id,CustomerSurname = r.Customer.Surname,EndDate = r.EndDate,
            StartDate = r.EndDate,RoomCapacity = r.RoomAttributes.Capacity.Value,
            RoomQuality = r.RoomAttributes.Quality.Name,RoomViewType = r.RoomAttributes.View.Name,TotalCost = r.TotalCost,CustomerId = r.CustomerId}).ToList();
        
            var resList = db.Reservations.Include(res=>res.Customer)
                .ToList();
            result.OrderBy(r=>r.StartDate);
            Debug.WriteLine(resList[0].Customer.Name);
        return result;
    }
    
    public void AddReservation(DisplayReservation item)
    {
        //status=1!!!!
        Reservation res = new Reservation();
        res.CustomerId = (int)item.CustomerId;
        res.TotalCost = item.TotalCost;
        res.StartDate = item.StartDate;
        res.EndDate = item.EndDate;
        
        db.RoomAttributes.Load();
        db.RoomCapacities.Load();
        db.RoomQualities.Load();
        db.RoomViewTypes.Load();
        
        res.RoomAttributesId = db.RoomAttributes.ToList().First(a=>a.Capacity.Value==item.RoomCapacity
        && a.Quality.Name==item.RoomQuality && a.View.Name==item.RoomViewType).Id;
        res.StatusId = db.ReservationStatuses.First(s => s.Status == "Активно").Id;

        db.Reservations.Add(res);
        db.SaveChanges();
    }

    public int GetId(DisplayReservation item)
    {
        
        return db.Reservations.ToList().First(r => r.StartDate == item.StartDate && r.EndDate==item.EndDate 
         && r.CustomerId==item.CustomerId && r.RoomAttributes.Capacity.Value==item.RoomCapacity 
         && r.RoomAttributes.Quality.Name==item.RoomQuality && r.RoomAttributes.View.Name==item.RoomViewType && r.TotalCost==item.TotalCost && r.Status.Status=="Активно").Id;
    }
}