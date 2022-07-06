using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using lab4_multiparadigma.Stores;
using lab4_multiparadigma.ViewModels;
using model;

namespace lab4_multiparadigma
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DobbleGamesSet dobbleGameSet = new();
            NavigationStore navigationStore = new();
            navigationStore.CurrentViewModel = new InitialViewModel(navigationStore);
            MainWindow MainWindow = new();
            MainWindowViewModel DataContext = new(navigationStore);
            MainWindow.DataContext = DataContext;
            MainWindow.Show();
        }
    }
}
