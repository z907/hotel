using System.Diagnostics;
using System.Runtime.CompilerServices;
using Hotel.VmEntities;
using Model.Entities;

using Microsoft.EntityFrameworkCore;
using ViewModel.VmEntities;

namespace ViewModel.Services;

public class ReservationService:BaseService,IService
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
            Debug.WriteLine(resList[0].Customer.Name);
       
        
        return result;

    }
    
    public void Add(IEntity item)
    {
        
    }
}