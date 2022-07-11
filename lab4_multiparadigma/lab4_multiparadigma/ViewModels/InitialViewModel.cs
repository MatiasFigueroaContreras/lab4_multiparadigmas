using lab4_multiparadigma.Commands;
using lab4_multiparadigma.Stores;
using model.DobbleGamesSetSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace lab4_multiparadigma.ViewModels
{
    public class InitialViewModel: ViewModelBase
    {
        /// <summary>
        ///  Almacen de navegacion el cual guarda la vista modelo actual y permite 
        ///     gestionar estas mediante eventos de cambio.
        /// </summary>
        public NavigationStore _navigationStore;
        /// <summary>
        ///  Conjunto de juegos dobble, que sera derivado a la vista modelo escogida
        /// </summary>
        public DobbleGamesSet _dobbleGameSet;

        /// <summary>
        /// Constructor que asigna los valores entregados a los correspondientes atributos.
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="dobbleGameSet"></param>
        public InitialViewModel(NavigationStore navigationStore, DobbleGamesSet dobbleGameSet)
        {
            this._navigationStore = navigationStore;
            this._dobbleGameSet = dobbleGameSet;
            
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos NavigateCreateGame
        /// </summary>
        public ICommand NavigateCreateGameCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(NavigateCreateGame));
            }
        }
        /// <summary>
        /// Manejador de eventos, encargado de cambiar la vista modelo actual, para que
        ///     sea derivada a la vista modelo CreateGame.
        /// </summary>
        /// <param name="o"></param>
        public void NavigateCreateGame(object? obj)
        {
            _navigationStore.CurrentViewModel = new CreateGameViewModel(_navigationStore, _dobbleGameSet);
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos NavigateCreatedGames
        /// </summary>
        public ICommand NavigateCreatedGamesCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(NavigateCreatedGames));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de cambiar la vista modelo actual, para que
        ///     sea derivada a la vista modelo CreatedGames.
        /// </summary>
        /// <param name="o"></param>
        public void NavigateCreatedGames(object? obj)
        {
            _navigationStore.CurrentViewModel = new CreatedGamesViewModel(_navigationStore, _dobbleGameSet);
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos CloseWindow
        /// </summary>
        public ICommand CloseWindowCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(CloseWindow));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de cerrar la ventana.
        /// </summary>
        /// <param name="o"></param>
        public void CloseWindow(object obj)
        {
            Window win = (Window)obj;
            win.Close();
        }
    }
}
