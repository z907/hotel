using Microsoft.EntityFrameworkCore;
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

    public List<ResAddService> GetAdditionalServicesForReservation(int id)
    {
        db.AdditionalServices.Load();
        return db.ResAddServices.Where(s => s.ReservationId == id).ToList();
    }

    public void RemakeServices(bool serviceBar, bool serviceSauna, bool serviceBilliards,int id)
    {
        db.ResAddServices.RemoveRange(db.ResAddServices.Where(r=>r.ReservationId==id));
        if (serviceBar)AddServiceStatement(id,GetAdditionalServiceByName("Безлимитный бар").Id);
        if (serviceSauna) AddServiceStatement(id,GetAdditionalServiceByName("Посещение сауны").Id);;
        if (serviceBilliards) AddServiceStatement(id,GetAdditionalServiceByName("Бильярд").Id);;
    }
}