using Hotel.VmEntities;
using Model.Context;
using Model.Entities;

namespace ViewModel.Services;

public class CostService:BaseService
{
    private AttributeService attrServ;
    private AdditionalServicesService serviceServ;

    public CostService()
    {
        attrServ = new AttributeService();
        serviceServ = new AdditionalServicesService();
    }

    public void SetDb(HotelDbContext cont)
    {
        attrServ.db = cont;
        serviceServ.db = cont;
    }
    public int CountTotalCost(DisplayReservation resToCount,bool ServiceBar,bool ServiceSauna,bool ServiceBilliards)
    {
        int roomQualityCost = (int)attrServ.GetQualityByName(resToCount.RoomQuality).Price;
        int roomViewCost = (int)attrServ.GetViewByName(resToCount.RoomViewType).Price;
        int roomCapacityCost = (int)attrServ.GetCapacityByValue((int)resToCount.RoomCapacity).Price;
        int duration =resToCount.EndDate.Value.DayNumber - resToCount.StartDate.Value.DayNumber+1;
        return duration * (roomQualityCost + roomViewCost + roomCapacityCost)+Convert.ToInt32(ServiceBar)*serviceServ.GetAdditionalServiceByName("Безлимитный бар").Price
            +Convert.ToInt32(ServiceSauna)*serviceServ.GetAdditionalServiceByName("Посещение сауны").Price
            +Convert.ToInt32(ServiceBilliards)*serviceServ.GetAdditionalServiceByName("Бильярд").Price;
    }
}