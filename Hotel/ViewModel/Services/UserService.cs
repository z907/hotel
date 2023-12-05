using Model.Entities;
using ViewModel.VmEntities;


namespace ViewModel.Services;

public class UserService:BaseService,IService
{
   public UserService():base()
   {
      
   }

   public VmUser CheckPassword(string username,string password)
   {
     
      var ul = db.Users.Where(u => u.Login == username && u.Password == password);
      if (!ul.Any()) return null;
      else
      {
         var ru = new VmUser(ul.First());
         return ru;
      }
   }

   public void Add(IEntity item)
   {
      
   }
}