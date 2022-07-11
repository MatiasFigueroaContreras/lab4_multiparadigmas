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
using model.DobbleGamesSetSpace;

namespace lab4_multiparadigma
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// En el inicio de la aplicacion crea el almacen de navegacion y el
        ///     conjunto de juegos Dobble que seran utilizados a lo largo
        ///     del programa, y asigna el contexto de la ventana principal
        ///     el cual va a estar dado por el ViewModel en el que se encuentre
        ///     esto hara que la vista sea la correspondiente a este ViewModel.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            NavigationStore navigationStore = new();
            DobbleGamesSet dobbleGamesSet = new();
            navigationStore.CurrentViewModel = new InitialViewModel(navigationStore, dobbleGamesSet);
            MainWindow MainWindow = new();
            MainWindowViewModel DataContext = new(navigationStore);
            MainWindow.DataContext = DataContext;
            MainWindow.Show();
        }
    }
}
