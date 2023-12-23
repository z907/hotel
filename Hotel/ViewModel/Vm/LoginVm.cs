using System.ComponentModel;
using System.Security.Principal;
using System.Windows.Input;
using ViewModel.Services;
using System.Windows;
using Model.Entities;
using ViewModel.VmEntities;

namespace Hotel.ViewModel;

public class LoginVm:BaseVm
{
    private string _userName;
    private string _password;
    private string _errorMessage;
    private UserService logServ;
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged(nameof(UserName));
        }
    }

    private bool _isLogViewVisible = true;
    public bool IsLogViewVisible
    {
        get => _isLogViewVisible;
        set
        {
            _isLogViewVisible = value; 
            OnPropertyChanged(nameof(IsLogViewVisible));
        }
    }
    private void ExecuteLoginCommand(object obj)
    {
       var u= logServ.CheckPassword(UserName, Password);
       if (u == null)
       {
           ErrMessage = "Wrong password or login";
       }
       else
       {
           Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(UserName), new string[]{logServ.GetRole(UserName)});
           IsLogViewVisible = false;
       }
       
    }
    private bool CanExecuteLoginCommand(object obj)
    {
        return true;
    }
    public LoginVm(UserService Service)
    {
        LoginCommand = new VmCommand(ExecuteLoginCommand,CanExecuteLoginCommand);
        logServ = Service;
    }
    public ICommand LoginCommand
    {
        get;
    }

    public string ErrMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value; 
            OnPropertyChanged(nameof(ErrMessage));
        }
    }
}