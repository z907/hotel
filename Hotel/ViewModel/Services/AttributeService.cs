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
}