using Model.Entities;

namespace ViewModel.VmEntities;

public class VmUser:IEntity
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public int? Role { get; set; }
     public string? RoleName  { get; set; }
    public VmUser(){}

    public VmUser(User u)
    {
        Id = u.Id;
        Login= u.Login;
        Password = u.Password;
        Role = u.Role;
        RoleName = u.RoleNavigation.Role;
    }
    
}