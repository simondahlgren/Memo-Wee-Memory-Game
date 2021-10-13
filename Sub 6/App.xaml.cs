using Sub_6.Stores;
using Sub_6.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sub_6
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();

            TimerStore timerStore = new TimerStore();
            //sets the intial view model to be loaded on startup, through the navstore
            navigationStore.CurrentViewModel = new StartViewModel(navigationStore, timerStore);

            //datacontext for mainwindow is set in MainViewModel
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)

            };
            MainWindow.Show();

            base.OnStartup(e);
        }

    }
}
