using Hotel.VmEntities;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace ViewModel.Services;

public class AttributeService:BaseService
{
    
    public List<RoomQuality> GetQualities()
    {
        var ls = db.RoomQualities.ToList();
        return ls;
    }
    public List<RoomViewType> GetViews()
    {
        var ls = db.RoomViewTypes.ToList();
        return ls;
    }
    public List<RoomCapacity> GetCapacities()
    {
        var ls = db.RoomCapacities.ToList();
        return ls;
    }

    public RoomCapacity GetCapacityByValue(int val)
    {
        return db.RoomCapacities.ToList().FirstOrDefault(c=>c.Value==val);
    }
    public RoomQuality GetQualityByName(string name)
    {
        return db.RoomQualities.ToList().FirstOrDefault(q=>q.Name==name);
    }
    public RoomViewType GetViewByName(string name)
    {
        return db.RoomViewTypes.ToList().FirstOrDefault(v=>v.Name==name);
    }
}