using Hotel.ViewModel.VmEntities;
using Microsoft.EntityFrameworkCore;

namespace ViewModel.Services;

public class ReportService:BaseService
{
    public int GetAverageOccupancyPercentage(int month,int year)
    {
        db.ReservationStatuses.Load();
        double sum = 0;
        int roomCount = db.Rooms.Count();
        int daysCount = DateTime.DaysInMonth(year, month);
        for (int i = 1; i <=daysCount; i++)
        {
            var resCount = db.Reservations.Count(r =>
                r.StartDate.Value <= new DateOnly(year, month, i)
                && r.EndDate.Value >= new DateOnly(year, month, i)
                && (r.Status.Status == "Оплачено" || r.Status.Status == "Не оплачено" ||
                    r.Status.Status == "Завершено"));
            sum += (double)resCount / roomCount*100;
        }
        return Convert.ToInt32((double)sum/daysCount);
    }

    public List<ReportInc> GetIncomePerDay(int month, int year)
    {
        List<ReportInc> result = new List<ReportInc>();
        int daysCount = DateTime.DaysInMonth(year, month);
        for (int i = 1; i <=daysCount; i++)
        {
            result.Add(new ReportInc
                {Day = i,
                    Income = (int)db.Payments.Where(p=>p.Date.Value.Day==i 
                 && p.Date.Value.Month==month
                 && p.Date.Value.Year==year).Sum(p=>p.Sum)
                });
        }
        return result;
    }
}