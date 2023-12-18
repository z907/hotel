using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;
using Hotel.ViewModel.VmEntities;
using Microsoft.EntityFrameworkCore;

namespace ViewModel.Services;

public class RoomService : BaseService
{
    private static BitmapImage LoadImage(byte[] imageData)
    {
        if (imageData == null || imageData.Length == 0) return null;
        var image = new BitmapImage();
        using (var mem = new MemoryStream(imageData))
        {
            mem.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = mem;
            image.EndInit();
        }
        image.Freeze();
        return image;
    }

    private bool IsRoomFree(int roomId)
    {
        db.ReservationStatuses.Load();
        return !db.Reservations.ToList().Exists(r =>
            (r.StartDate.Value.Year == DateTime.Now.Year && r.StartDate.Value.Month ==DateTime.Now.Month&& r.StartDate.Value.Day == DateTime.Now.Day) && r.RoomId==roomId
            && (r.Status.Status=="Оплачено" ||r.Status.Status=="Не оплачено"));
    }
    public List<DisplayRoom> GetRoomsByType(int attrId)
    {
        return db.Rooms.ToList().Where(r => r.RoomAttributesId == attrId && IsRoomFree(r.Id))
            .Select(r => new DisplayRoom { Id = r.Id, Image = LoadImage(r.Image) , Number = r.Number }).ToList();
    }
}