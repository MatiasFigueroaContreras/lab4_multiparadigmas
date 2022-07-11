using lab4_multiparadigma.Commands;
using lab4_multiparadigma.Resources.Helpers;
using lab4_multiparadigma.Stores;
using model.DobbleGameSpace;
using model.DobbleGamesSetSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace lab4_multiparadigma.ViewModels
{
    public class CreateGameViewModel: ViewModelBase
    {
        /// <summary>
        ///  Almacen de navegacion el cual guarda la vista modelo actual y permite 
        ///     gestionar estas mediante eventos de cambio.
        /// </summary>
        public NavigationStore _navigationStore;

        /// <summary>
        ///  Conjunto de juegos dobble, con el que se va a trabajar, agregando
        ///     nuevos juegos dobble a este.
        /// </summary>
        public DobbleGamesSet _dobbleGamesSet;

        /// <summary>
        /// Nombre del juego que se va a agregar al conjunto dobble.
        /// </summary>
        private String _gameName;

        /// <summary>
        /// Numero de cartas que va a tener el juego a agregar al conjunto dobble.
        /// </summary>
        private int _numberCards;

        /// <summary>
        /// Modo de juego que va a tener el juego a agregar al conjunto dobble.
        /// </summary>
        private String _gameMode;

        /// <summary>
        /// Numero de elementos que va a tener el juego a agregar al conjunto dobble.
        /// </summary>
        private int _numberElements;

        /// <summary>
        /// Tiempo en segundos que va a estar asociado al juego a agregar al conjunto dobble.
        /// </summary>
        private int _gameTime;

        /// <summary>
        /// Maximos jugadores que va a tener el juego a agregar al conjunto dobble.
        /// </summary>
        private int _maxPlayers;

        /// <summary>
        /// Constructor que asigna los valores entregados a los correspondientes atributos.
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="dobbleGamesSet"></param>
        public CreateGameViewModel(NavigationStore navigationStore, DobbleGamesSet dobbleGamesSet)
        {
            this._navigationStore = navigationStore;
            this._dobbleGamesSet = dobbleGamesSet;
        }

        /// <summary>
        /// Permite obtener el nombre de juego y modificar este disparando
        ///     el evento de que este valor se cambio para que sea actualizado.
        /// </summary>
        public string GameName
        {
            get
            {
                return _gameName;
            }
            set
            {
                if(value != null && !value.Equals(_gameName))
                {
                    _gameName = value;
                    OnPropertyChanged(nameof(GameName));
                }
            }
        }


        /// <summary>
        /// Permite obtener el numero de cartas y modificar este disparando
        ///     el evento de que este valor se cambio para que sea actualizado, manejando
        ///     que lo ingresado sea un valor entero.
        /// </summary>
        public string NumberCards
        {
            get
            {
                return Convert.ToString(_numberCards);
            }
            set
            {
                if(value != null)
                {
                    try
                    {
                        _numberCards = Convert.ToInt32(value);
                        OnPropertyChanged(nameof(NumberCards));
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Debe ingresar un numero entero.");
                    }
                }
            }
        }

        /// <summary>
        /// Permite obtener el modo de juego y modificar este disparando
        ///     el evento de que este valor se cambio para que sea actualizado.
        /// </summary>
        public string GameMode
        {
            get { return _gameMode; }
            set
            {
              if (value != null && !value.Equals(_gameMode))
              {
                    this._gameMode = value;
                    OnPropertyChanged(nameof(GameMode));
              }
            }
        }

        /// <summary>
        /// Permite obtener el numero de elementos y modificar este disparando
        ///     el evento de que este valor se cambio para que sea actualizado, manejando
        ///     que lo ingresado sea un valor entero.
        /// </summary>
        public string NumberElements
        {
            get
            {
                return Convert.ToString(_numberElements);
            }
            set
            {
                if (value != null)
                {
                    try
                    {
                        _numberElements = Convert.ToInt32(value);
                        OnPropertyChanged(nameof(NumberElements));
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Debe ingresar un numero entero.");
                    }
                }
            }
        }

        /// <summary>
        /// Permite obtener el tiempo en segundos y modificar este disparando
        ///     el evento de que este valor se cambio para que sea actualizado, manejando
        ///     que lo ingresado sea un valor entero.
        /// </summary>
        public string GameTime
        {
            get
            {
                return Convert.ToString(_gameTime);
            }
            set
            {
                if (value != null)
                {
                    try
                    {
                        _gameTime = Convert.ToInt32(value);
                        OnPropertyChanged(nameof(GameTime));
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Debe ingresar un numero entero.");
                    }
                }
            }
        }

        /// <summary>
        /// Permite obtener el maximo de jugadores y modificar este disparando
        ///     el evento de que este valor se cambio para que sea actualizado, manejando
        ///     que lo ingresado sea un valor entero.
        /// </summary>
        public String MaxPlayers
        {
            get
            {
                return Convert.ToString(_maxPlayers);
            }
            set
            {
                if(value != null)
                {
                    try
                    {
                        _maxPlayers = Convert.ToInt32(value);
                        OnPropertyChanged(nameof(MaxPlayers));
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Debe ingresar un numero.");
                    }
                }
            }
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos AddCard.
        /// </summary>
        public ICommand AddCardCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(AddCard));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de darle los valores al DobbleGameCardsSet para
        ///     que agrega el juego al conjunto, esto verificando que no se produscan
        ///     excepciones en la creacion de este.
        /// </summary>
        /// <param name="o"></param>
        public void AddCard(object? o)
        {
            if(_numberElements > 8)
            {
                MessageBox.Show("Numero de elemntos no disponible, se aceptan menores a 8.");
            }
            else
            {
                try
                {
                    List<string> elements = ImageElements.Elements;
                    int totalCards = DobbleGame.totalCardsNumElements(_numberElements);
                    Random random = new Random();
                    int i = (int)random.NextInt64(elements.Count - totalCards);
                    List<string> dobbleGameElements = elements.GetRange(i, totalCards);
                    _dobbleGamesSet.add(_gameName, _maxPlayers, _gameMode, dobbleGameElements, _numberElements, _numberCards, _gameTime);
                    MessageBox.Show("Juego creado con exito!");
                    _navigationStore.CurrentViewModel = new DobbleGameViewModel(_navigationStore, _dobbleGamesSet, _dobbleGamesSet.length());
                }
                catch (DobbleGamesSetException e)
                {
                    //Mensaje con errror

                    MessageBox.Show("Error " + e.Code + ": " + e.Message);
                }
                catch (DobbleGameException e)
                {
                    //Mensaje con error
                    MessageBox.Show("Error " + e.Code + ": " + e.Message);
                }
            }
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
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
        /// Manejador de eventos, encargado de cambiar la vista actual, para que
        ///     sea derivada a la vista inicial.
        /// </summary>
        /// <param name="o"></param>
        public void NavigateBack(object? obj)
        {
            _navigationStore.CurrentViewModel = new InitialViewModel(_navigationStore, _dobbleGamesSet);
        }

    }
}
