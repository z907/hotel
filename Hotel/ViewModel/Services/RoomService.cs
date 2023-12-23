using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using Hotel.ViewModel.VmEntities;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace ViewModel.Services;

public class RoomService : BaseService
{
    public static BitmapImage LoadImage(byte[] imageData)
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
            (r.StartDate<=new DateOnly(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day)
             && r.EndDate>=new DateOnly(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day)) && r.RoomId==roomId
            && (r.Status.Status=="Оплачено" ||r.Status.Status=="Не оплачено"));
    }
    //r.StartDate.Value.Year == DateTime.Now.Year && r.StartDate.Value.Month ==DateTime.Now.Month&& r.StartDate.Value.Day == DateTime.Now.Day
    public List<DisplayRoom> GetRoomsByType(int attrId)
    {
        return db.Rooms.ToList().Where(r => r.RoomAttributesId == attrId && IsRoomFree(r.Id)&& (bool)r.Valid)
            .Select(r => new DisplayRoom { Id = r.Id, Image = LoadImage(r.Image) , Number = r.Number }).ToList();
    }

    public List<DisplayRoom> GetAllRooms()
    {
        db.RoomAttributes.Load();
        db.RoomCapacities.Load();
        db.RoomQualities.Load();
        db.RoomViewTypes.Load();
        return db.Rooms.Select(r=>new DisplayRoom
        {
            Id=r.Id,
            Number = r.Number,
            Capacity = r.RoomAttributes.Capacity.Value,
            Quality = r.RoomAttributes.Quality.Name,
            ViewType = r.RoomAttributes.View.Name,
            Valid = ((bool)r.Valid?"Да" :"Нет"),
            Price = r.RoomAttributes.Capacity.Price+r.RoomAttributes.Quality.Price+r.RoomAttributes.View.Price,
            Image = LoadImage(r.Image)
        }).ToList();
    }

    public void ToggleValid(int roomId)
    {
        var r = db.Rooms.Find(roomId);
        r.Valid = !r.Valid;
        db.Rooms.Update(r);
        db.SaveChanges();
    }

    public void AddRoom(DisplayRoom toAdd)
    {
        db.RoomAttributes.Load();
        db.RoomQualities.Load();
        db.RoomCapacities.Load();
        db.RoomViewTypes.Load();
        int aId;
        int c = db.RoomCapacities.First(c=>c.Value==toAdd.Capacity).Id;
        int v = db.RoomViewTypes.First(v=>v.Name==toAdd.ViewType).Id;
        int q = db.RoomQualities.First(q=>q.Name==toAdd.Quality).Id;
        var attr = db.RoomAttributes.Where(a => a.CapacityId == c && a.QualityId == q && a.ViewId == v);
        if (attr.Any()) aId = attr.First().Id;
        else
        {
            db.RoomAttributes.Add(new RoomAttribute
            {
                ViewId = v, CapacityId = c, QualityId = q,
                Price = (db.RoomCapacities.Find(c).Price + db.RoomQualities.Find(q).Price +
                         db.RoomViewTypes.Find(v).Price)
            });
            db.SaveChanges();
            aId=db.RoomAttributes.First(a => a.CapacityId == c && a.QualityId == q && a.ViewId == v).Id;
        }
        
        var r = new Room { Number = toAdd.Number.Value,Valid = true,RoomAttributesId = aId,Image = ImageToByte(toAdd.Image)};
        db.Rooms.Add(r);
        db.SaveChanges();
    }
    public bool DeleteRoom(int roomId)
    {
        db.Rooms.Remove(db.Rooms.Find(roomId));
        try
        {
            db.SaveChanges();
        }
        catch
        {
            return false;
        }
        return true;
    }

    public bool CheckNumber(int num)
    {
        return db.Rooms.ToList().Exists(r => r.Number == num);
    }
    public static byte[] ImageToByte(BitmapImage img)
    {
        PngBitmapEncoder converter = new PngBitmapEncoder();
        byte[] data;
       
        converter.Frames.Add(BitmapFrame.Create(img));
        using(MemoryStream ms = new MemoryStream())
        {
            converter.Save(ms);
            data = ms.ToArray();
        }

        return data;
    }
}