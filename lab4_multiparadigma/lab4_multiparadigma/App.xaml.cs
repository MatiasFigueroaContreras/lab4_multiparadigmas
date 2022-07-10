using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using lab4_multiparadigma.Resources.Helpers;
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
            NavigationStore navigationStore = new();
            DobbleGamesSet dobbleGamesSet = new();
            dobbleGamesSet.add("Prueba", 1, "Stack Player VS CPU", ImageElements.Elements, 3, 7, 100);
            dobbleGamesSet.add("Prueba 2", 1, "Stack Player VS CPU", ImageElements.Elements, 3, 7, 200);
            dobbleGamesSet.add("Prueba 3", 1, "Stack Player VS CPU", ImageElements.Elements, 3, 7, 100);
            dobbleGamesSet.getGame(2).register("ManttiuS");
            dobbleGamesSet.getGame(2).start();
            dobbleGamesSet.getGame(2).finish();
            dobbleGamesSet.getGame(3).register("ManttiuS");
            dobbleGamesSet.getGame(3).start();

            navigationStore.CurrentViewModel = new InitialViewModel(navigationStore, dobbleGamesSet);
            MainWindow MainWindow = new();
            MainWindowViewModel DataContext = new(navigationStore);
            MainWindow.DataContext = DataContext;
            MainWindow.Show();
        }
    }
}
