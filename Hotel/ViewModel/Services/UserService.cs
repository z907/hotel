using Microsoft.EntityFrameworkCore;
using Model.Entities;
using ViewModel.VmEntities;


namespace ViewModel.Services;

public class UserService:BaseService
{
   public UserService():base()
   {
      
   }

   public VmUser CheckPassword(string username,string password)
   {
     db.UserRoles.Load();
      var ul = db.Users.Where(u => u.Login == username && u.Password == password);
      if (!ul.Any()) return null;
         var ru = new VmUser(ul.First());
         return ru;
   }

   public string GetRole(string login)
   {
       db.UserRoles.Load();
       return db.Users.First(u => u.Login == login).RoleNavigation.Role;
   }
  
}