using Model.Entities;

namespace ViewModel.Services;

public class AdditionalServicesService:BaseService
{
    public List<AdditionalService> GetAdditionalServices()
    {
        return db.AdditionalServices.ToList();
    }
    public AdditionalService GetAdditionalServiceByName(string name)
    {
        return db.AdditionalServices.ToList().First(s=>s.Name==name);
    }

    public void AddServiceStatement(int resId,int servId)
    {
        ResAddService newItem = new ResAddService();
        newItem.ReservationId = resId;
        newItem.ServiceId = servId;
        db.ResAddServices.Add(newItem);
        db.SaveChanges();
    }
}