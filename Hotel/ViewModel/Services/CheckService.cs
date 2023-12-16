using Hotel.VmEntities;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using ViewModel.VmEntities;

namespace ViewModel.Services;

public class CheckService:BaseService,IService
{
    public bool CheckForFreeRooms(DisplayReservation toCheck)
    {
        RoomAttribute attr = new RoomAttribute();
        attr.CapacityId = db.RoomCapacities.Where(c=>c.Value==toCheck.RoomCapacity).Select(c=>c.Id).FirstOrDefault();
        attr.QualityId = db.RoomQualities.Where(q=>q.Name==toCheck.RoomQuality).Select(q=>q.Id).FirstOrDefault();
        attr.ViewId = db.RoomViewTypes.Where(v=>v.Name==toCheck.RoomViewType).Select(v=>v.Id).FirstOrDefault();
        RoomAttribute FoundAttr = db.RoomAttributes.Where(v=>v.ViewId==attr.ViewId).
            Where(q=>q.QualityId==attr.QualityId).
            Where(c=>c.CapacityId==attr.CapacityId).
            FirstOrDefault();
        if (FoundAttr == null) return false;
        int roomcount = db.Rooms.Where(r=>r.RoomAttributesId==FoundAttr.Id).Count();
        db.ReservationStatuses.Load();
        List<Reservation> resList = db.Reservations.Where(r => r.RoomAttributesId == FoundAttr.Id).Where(r=>r.Status.Status=="Активно").ToList();
        bool flag = false;
        int counter = 0;
        
        for (DateOnly d = (DateOnly)toCheck.StartDate; d <= (DateOnly)toCheck.EndDate; d=d.AddDays(1))
        {
            counter = 0;
            foreach (var r in resList)
            {
                if (d >= r.StartDate && d <= r.EndDate) counter++;
            }
            if (counter >= roomcount)
            {
                flag = true;
                break;
            }
        }
        return !flag;
    }

    
    
    public void Add(IEntity item)
    {
        
    }
}