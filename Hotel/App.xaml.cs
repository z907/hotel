using System.Configuration;
using System.Data;
using System.Windows;
using Hotel.ViewModel;
using ViewModel.Services;
using Ninject;
namespace Hotel;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected void Application_Startup(object sender, StartupEventArgs e)
    {
        
        var us = GlobalKernel.Kernel.Get<UserService>();
        var ls = new LoginVm(us);
        
        var logWindow = new LoginWindow();
        logWindow.DataContext = ls;
        logWindow.Show();
        logWindow.IsVisibleChanged += (s, ev) =>
        {
            if (logWindow.IsVisible == false && logWindow.IsLoaded)
            {
                var mainWin = new MainWindow();
                mainWin.Show();
                logWindow.Close();
            }
        };
    }
}