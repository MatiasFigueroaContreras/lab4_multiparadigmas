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
using System.Windows.Threading;

namespace lab4_multiparadigma.ViewModels
{
    public class DobbleGameViewModel : ViewModelBase
    {
        /// <summary>
        ///  Almacen de navegacion el cual guarda la vista modelo actual y permite 
        ///     gestionar estas mediante eventos de cambio.
        /// </summary>
        private NavigationStore _navigationStore;

        /// <summary>
        ///  Conjunto de juegos dobble, con los que se va a trabajar para obtener los
        ///     segundos que le pertenecen al juego presente.
        /// </summary>
        private DobbleGamesSet _dobbleGamesSet;

        /// <summary>
        /// Objeto Timer encargado de disparar el evento Tick cada un segundo
        ///     al manejador de eventos updateTimer.
        /// </summary>
        private DispatcherTimer _timer;

        /// <summary>
        /// Indice en el que se encuentra el juego y segundos en el conjunto de juegos Dobble
        /// </summary>
        private int _dobbleGameIndex;

        /// <summary>
        /// Juego Dobble con el cual va a trabajar la vista modelo.
        /// </summary>
        private DobbleGame _dobbleGame;

        /// <summary>
        /// Jugador a registrar en el juego Dobble.
        /// </summary>
        private string _playerToRegister;

        /// <summary>
        /// Jugador del cual se desea obtener su puntaje.
        /// </summary>
        private string _playerToGetScore;

        /// <summary>
        /// Segundos actuales en el timer.
        /// </summary>
        private int _seconds;

        /// <summary>
        /// Permite saber si el Panel/menu lateral esta abierto o no
        ///     y asi abrir este o cerrarlo dependiendo de la accion
        ///     del usuario.
        /// </summary>
        private bool _isSidePanelVisible;

        /// <summary>
        /// Constructor que asigna los valores entregados a los correspondientes atributos,
        ///     y ademas inicia valores que pueden ser mostrados en la vista.
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="dobbleGamesSet"></param>
        /// <param name="dobbleGameIndex"></param>
        public DobbleGameViewModel(NavigationStore navigationStore, DobbleGamesSet dobbleGamesSet, int dobbleGameIndex)
        {
            _navigationStore = navigationStore;
            _dobbleGamesSet = dobbleGamesSet;
            _dobbleGameIndex = dobbleGameIndex;
            _dobbleGame = _dobbleGamesSet.getGame(_dobbleGameIndex);
            _timer = new();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += updateTimer;
            if (IsCardsInPlay)
            {
                _timer.Start();
            }
            _seconds = _dobbleGamesSet.getSeconds(_dobbleGameIndex);


            updateDobbleGameInfo();
            _isSidePanelVisible = (!IsStartedGame || IsFinishedGame);
        }

        /// <summary>
        /// Permite obtener el nombre de juego.
        /// </summary>
        public string GameName
        {
            get { return _dobbleGame.getGameName(); }
        }

        /// <summary>
        /// Permite obtener el modo de juego.
        /// </summary>
        public string GameMode
        {
            get { return _dobbleGame.getNameOfMode() + " " + _dobbleGame.getVersionMode(); }
        }

        /// <summary>
        /// Permite obtener el estado del juego.
        /// </summary>
        public string GameStatus
        {
            get { return _dobbleGame.getStatus(); }
        }

        /// <summary>
        /// Permite obtener el nombre del jugador que tiene el turno actual.
        /// </summary>
        public string? PlayerTurn
        {
            get { return "Turno de " + _dobbleGame.whoseTurnIsIt(); }
        }

        /// <summary>
        /// Permite obtener el puntaje del jugador que tiene el turno actual.
        /// </summary>
        public string? PlayerScore
        {
            get
            {
                string? playerTurn = _dobbleGame.whoseTurnIsIt();
                if(playerTurn != null)
                {
                    string score = _dobbleGame.getScore(playerTurn).ToString();
                    return "Puntaje: " + score;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Permite obtener el nombre de juegador a registrar y modificar 
        /// este disparando el evento de que este valor se cambio para que sea actualizado.
        /// </summary>
        public string PlayerToRegister
        {
            get { return _playerToRegister; }
            set
            {
                _playerToRegister = value;
                OnPropertyChanged(nameof(PlayerToRegister));
            }
        }

        /// <summary>
        /// Permite obtener el nombre del juegador a obtener su puntaje y modificar 
        ///     este disparando el evento de que este valor se cambio para que sea actualizado.
        /// </summary>
        public string PlayerToGetScore
        {
            get { return _playerToGetScore; }
            set
            {
                _playerToGetScore = value;
                OnPropertyChanged(nameof(PlayerToGetScore));
            }
        }


        /// <summary>
        /// Permite obtener el tiempo del temporizador.
        /// </summary>
        public string Timer
        {
            get
            {
                TimeSpan time = TimeSpan.FromSeconds(_seconds);
                return time.ToString(@"mm\:ss");
            }
        }

        /// <summary>
        /// Permite obtener las opciones de juego.
        /// </summary>
        public List<string>? PlaysOptions
        {
            get
            {
                if (IsStartedGame)
                {
                    List<string> playsOptions = _dobbleGamesSet.getGame(_dobbleGameIndex).getPlaysOptions().ToList();
                    if (IsCardsInPlay)
                    {
                        playsOptions.Remove("Elegir elemento en comun");
                        _timer.Start();
                    }
                    return playsOptions;

                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Permite saber si el juego esta iniciado.
        /// </summary>
        public bool IsStartedGame
        {
            get { return _dobbleGame.isStarted(); }
        }


        /// <summary>
        /// Permite saber si el juego no esta iniciado.
        /// </summary>
        public bool IsNotStartedGame
        {
            get { return (!IsStartedGame && !IsFinishedGame); }
        }

        /// <summary>
        /// Permite saber si el juego esta iniciado o terminado.
        /// </summary>
        public bool IsStartedOrFinishedGame
        {
            get { return IsStartedGame || IsFinishedGame; }
        }

        /// <summary>
        /// Permite saber si el juego esta terminado.
        /// </summary>
        public bool IsFinishedGame
        {
            get { return _dobbleGame.isFinished(); }
        }

        /// <summary>
        /// Permite saber si el juego no esta iniciado a la vista.
        /// </summary>
        public bool IsSidePanelVisible
        {
            get { return _isSidePanelVisible; }
            set
            {
                _isSidePanelVisible = value;
                OnPropertyChanged(nameof(IsSidePanelVisible));
            }
        }

        /// <summary>
        /// Permite saber si las cartas estan en jugo.
        /// </summary>
        public bool IsCardsInPlay
        {
            get
            {
                return _dobbleGame.getCardsInPlay().Count != 0;
            }
        }

        /// <summary>
        /// Permite obtener las cartas en juego 1.
        /// </summary>
        public List<string>? CardElements1
        {
            get
            {
                if (IsCardsInPlay)
                {
                    return _dobbleGame.getCardsInPlay()[0];
                }
                return null;
            }
        }

        /// <summary>
        /// Permite obtener las cartas en juego 2.
        /// </summary>
        public List<string>? CardElements2
        {
            get
            {
                if (IsCardsInPlay)
                {
                    return _dobbleGame.getCardsInPlay()[1];
                }
                return null;
            }
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos ShowPanel.
        /// </summary>
        public ICommand ShowPanelCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(ShowPanel));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de mostrar el panel/menu lateral, 
        ///     cambiando la visibilidad de este.
        /// </summary>
        /// <param name="o"></param>
        public void ShowPanel(object? o)
        {
            IsSidePanelVisible = true;
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos HidePanel.
        /// </summary>
        public ICommand HidePanelCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(HidePanel));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de esconder el panel/menu lateral, 
        ///     cambiando la visibilidad de este.
        /// </summary>
        /// <param name="o"></param>
        public void HidePanel(object? o)
        {
            IsSidePanelVisible = false;
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos RegisteredPlayers.
        /// </summary>
        public ICommand RegisteredPlayersCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(RegisteredPlayers));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de mostrar un mensaje con los
        ///     jugadores registrados en el juego.
        /// </summary>
        /// <param name="o"></param>
        public void RegisteredPlayers(object o)
        {
            MessageBox.Show("Jugadores Registrados:\n" + _dobbleGame.registeredPlayers());
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos NavigateBack.
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
        ///     sea derivada a la vista modelo CreatedGames.
        /// </summary>
        /// <param name="o"></param>
        public void NavigateBack(object? o)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                Play("Pasar");
            }
            _navigationStore.CurrentViewModel = new CreatedGamesViewModel(_navigationStore, _dobbleGamesSet);
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos RegisterPlayer.
        /// </summary>
        public ICommand RegisterPlayerCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(RegisterPlayer));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de registrar un jugador dado
        ///     en el juego.
        /// </summary>
        /// <param name="o"></param>
        public void RegisterPlayer(object? o)
        {
            try
            {
                _dobbleGame.register(_playerToRegister);
                MessageBox.Show("Jugador registrado con exito!!");
                PlayerToRegister = "";
            }
            catch(DobbleGameException e)
            {
                MessageBox.Show("Error " + e.Code + ": " + e.Message);
            }
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos GeneralGameInformation.
        /// </summary>
        public ICommand GeneralGameInformationCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(GeneralGameInformation));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de mostrar un mensaje con
        ///     la informacion general del juego.
        /// </summary>
        /// <param name="o"></param>
        public void GeneralGameInformation(object? o)
        {
            MessageBox.Show(_dobbleGame.ToString());
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos GetPlayerScore.
        /// </summary>
        public ICommand GetPlayerScoreCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(GetPlayerScore));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de mostrar un mensaje con el
        ///     puntaje de un jugador dado.
        /// </summary>
        /// <param name="o"></param>
        public void GetPlayerScore(object? o)
        {
            try
            {
                string score = _dobbleGame.getScore(_playerToGetScore).ToString();
                MessageBox.Show("El puntaje del jugador " + _playerToGetScore + " es: " + score);
                PlayerToGetScore = "";
            }
            catch (DobbleGameException e)
            {
                MessageBox.Show("Error " + e.Code + ": " + e.Message);
            }
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos StartGame.
        /// </summary>
        public ICommand StartGameCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(StartGame));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de empezar el juego.
        /// </summary>
        /// <param name="o"></param>
        public void StartGame(object? o)
        {
            try
            {
                _dobbleGame.start();
                updateDobbleGameInfo();
                HidePanel(o);
            }
            catch (DobbleGameException e)
            {
                MessageBox.Show("Error " + e.Code + ": " + e.Message);
            }
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos FinishGame.
        /// </summary>
        public ICommand FinishGameCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(FinishGame));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de terminar el juego.
        /// </summary>
        /// <param name="o"></param>
        public void FinishGame(object? o)
        {
            try
            {
                _dobbleGame.finish();
                updateDobbleGameInfo();
                IsSidePanelVisible = false;
                MessageBox.Show("Juego terminado, resultados en el menú del juego.");
            }
            catch (DobbleGameException e)
            {
                MessageBox.Show("Error " + e.Code + ": " + e.Message);
            }
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos GameResult.
        /// </summary>
        public ICommand GameResultsCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(GameResults));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de mostrar un mensaje con los
        ///     resultados del juego.
        /// </summary>
        /// <param name="o"></param>
        public void GameResults(object? o)
        {
            try
            {
                MessageBox.Show(_dobbleGame.getGameResults()); 
            }
            catch(DobbleGameException e)
            {
                MessageBox.Show("Error " + e.Code + ": " + e.Message);
            }
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos Play.
        /// </summary>
        public ICommand PlayCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(Play));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de realizar una jugada dada
        ///     en el juego Dobble.
        /// </summary>
        /// <param name="o"></param>
        public void Play(object o)
        {
            string option = (string)o;
            try
            {
                _dobbleGame.play(option);
            }
            catch(DobbleGameException e)
            {
                if (e.Code == 501)
                {
                    MessageBox.Show(e.Message + " resultados en el menú del juego.");
                }
                IsSidePanelVisible = true;
            }
            updateDobbleGameInfo();
        }

        /// <summary>
        /// Comando que puede ser usado en una vista para delegar la accion al
        ///     manejador de eventos PlayElement.
        /// </summary>
        public ICommand PlayElementCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(PlayElement));
            }
        }

        /// <summary>
        /// Manejador de eventos, encargado de realizar la jugada 
        ///     de elegir elemento en comun, con un elemento dado.
        /// </summary>
        /// <param name="o"></param>
        public void PlayElement(object o)
        {
            string element = (string)o;
            string[] data = {element};
            _dobbleGame.play("Elegir elemento en comun", data);
            updateDobbleGameInfo();
        }

        /// <summary>
        /// Manejador de eventos, encargado de actualizar el tiempo
        ///     del temporizador, y parar este si se llega a 0
        ///     o no hay cartas en juego, ademas reestablece los segundos
        ///     a los indicados para el juego.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        public void updateTimer(object? sender, EventArgs? e)
        {
            if (!IsCardsInPlay)
            {
                _timer.Stop();
                _seconds = _dobbleGamesSet.getSeconds(_dobbleGameIndex);
            }
            else if (_seconds == 0)
            {
                _timer.Stop();
                _seconds = _dobbleGamesSet.getSeconds(_dobbleGameIndex);
                Play("Pasar");
            }
            else
            {
                _seconds -= 1;
            }

            OnPropertyChanged(nameof(Timer));
        }

        /// <summary>
        /// Encargado de disparar eventos de cambio en los atributos
        ///     que tienen informacion del juego.
        /// </summary>
        public void updateDobbleGameInfo()
        {
            OnPropertyChanged(nameof(GameStatus));
            OnPropertyChanged(nameof(PlayerTurn));
            OnPropertyChanged(nameof(PlayerScore));
            OnPropertyChanged(nameof(PlaysOptions));
            OnPropertyChanged(nameof(IsStartedGame));
            OnPropertyChanged(nameof(IsNotStartedGame));
            OnPropertyChanged(nameof(IsStartedOrFinishedGame));
            OnPropertyChanged(nameof(IsFinishedGame));
            OnPropertyChanged(nameof(IsCardsInPlay));
            OnPropertyChanged(nameof(CardElements1));
            OnPropertyChanged(nameof(CardElements2));
        }
    }
}
