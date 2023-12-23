using Model.Entities;

namespace ViewModel.Services;

public class PayService:BaseService
{
   public int CountLeftover(int resId)
   {
      int totalCost =(int) db.Reservations.Find(resId).TotalCost;
      var payList = db.Payments.Where(p => p.ReservationId == resId);
      int sum = Convert.ToInt32(payList.Sum(p => p.Sum));
      return totalCost - sum;
   }

   public void Pay(int chId, int sum)
   {
      db.Payments.Add(new Payment {Date =DateOnly.FromDateTime(DateTime.Today),Sum = sum,ReservationId = chId});
      db.SaveChanges();
      if (CountLeftover(chId) == 0)
      {
         var r = db.Reservations.Find(chId);
         r.StatusId=db.ReservationStatuses.First(s => s.Status == "Оплачено").Id;
         db.Reservations.Update(r);
      }
      db.SaveChanges();
   }
}