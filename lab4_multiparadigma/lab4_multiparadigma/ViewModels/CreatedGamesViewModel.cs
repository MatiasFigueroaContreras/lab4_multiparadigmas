using lab4_multiparadigma.Commands;
using lab4_multiparadigma.Stores;
using model.DobbleGameSpace;
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
    /// <summary>
    /// Clase encargada de obtener los datos del DobbleGamesSet y proporcionarlos a 
    /// la vista correspondiente, además encargada de manejar los eventos que sean 
    /// disparados en la vista.
    /// </summary>
    public class CreatedGamesViewModel : ViewModelBase
    {
        /// <summary>
        ///  Almacen de navegacion el cual guarda la vista modelo actual y permite 
        ///     gestionar estas mediante eventos de cambio.
        /// </summary>
        public NavigationStore _navigationStore;
        /// <summary>
        ///  Conjunto de juegos dobble, con los que se va a trabajar para comunicarlo e
        ///     interactuar con la vista
        /// </summary>
        private DobbleGamesSet _dobbleGamesSet;

        /// <summary>
        /// Constructor que asigna los valores entregados a los correspondientes atributos.
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="dobbleGamesSet"></param>
        public CreatedGamesViewModel(NavigationStore navigationStore, DobbleGamesSet dobbleGamesSet)
        {
            this._navigationStore = navigationStore;
            this._dobbleGamesSet = dobbleGamesSet;
        }

        /// <summary>
        /// Atributo que representa la informacion necesaria que puede ser mostrada en la 
        ///     vista con el nombre del juego, el modo de juego, un estado base, y el indice
        ///     del juego en el conjunto.
        /// </summary>
        public List<List<string>> DobbleGamesSet
        {
            get
            {
                List<List<string>> infoGames = new();
                foreach (DobbleGame dG in _dobbleGamesSet.games)
                {
                    List<string> game = new();
                    game.Add(dG.getGameName());
                    game.Add(dG.getNameOfMode() + " " + dG.getVersionMode());
                    if (dG.isFinished())
                    {
                        game.Add("Terminado");
                    }
                    else if (dG.isStarted())
                    {
                        game.Add("Iniciado");
                    }
                    else
                    {
                        game.Add("No Iniciado");
                    }
                    game.Add(_dobbleGamesSet.getGameIndex(dG).ToString());
                    infoGames.Add(game);
                }
                return infoGames;
            }
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos NavigateDobbleGame
        /// </summary>
        public ICommand NavigateDobbleGameCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(NavigateDobbleGame));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de cambiar la vista actual, para que
        ///     sea derivada a la vista del juego Dobble.
        /// </summary>
        /// <param name="dobbleGameIndex">Indice del juego a mandar a la vista</param>
        public void NavigateDobbleGame(object dobbleGameIndex)
        {
            string indexString = (string)dobbleGameIndex;
            int index = Int32.Parse(indexString);
            _navigationStore.CurrentViewModel = new DobbleGameViewModel(_navigationStore, _dobbleGamesSet, index);
        }

        /// <summary>
        ///  Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos NavigateBack
        /// </summary>
        public ICommand NavigateBackCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(NavigateBack));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de cambiar la vista modelo actual, para que
        ///     sea derivada a la vista modelo Initial.
        /// </summary>
        /// <param name="o"></param>
        public void NavigateBack(object? o)
        {
            _navigationStore.CurrentViewModel = new InitialViewModel(_navigationStore, _dobbleGamesSet);
        }
    }
}
