using KeyDash.Models;
using KeyDash.MVVM;
using KeyDash.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace KeyDash;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var eventbus = new EventBus();
        var game = new Game(eventbus, Modes.None);
       
        var vmtopmenu = new ViewModelTopMenu(game, eventbus);
        var vmmainplace = new ViewModelMainPlace();
        var vmtimer = new ViewModelTimer(eventbus);
        var vmMain = new ViewModelMainWindow(game, vmmainplace,vmtopmenu,vmtimer, eventbus);
        var mainwindow = new MainWindow();
        mainwindow.DataContext = vmMain;
        mainwindow.Show();

       
    }
}

