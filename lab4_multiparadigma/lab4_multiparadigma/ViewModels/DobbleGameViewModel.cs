using lab4_multiparadigma.Commands;
using lab4_multiparadigma.Stores;
using model;
using model.DobbleGameSpace;
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
        private NavigationStore _navigationStore;
        private DobbleGamesSet _dobbleGamesSet;
        private DispatcherTimer _timer;
        private int _dobbleGameIndex;
        private DobbleGame _dobbleGame;
        private string _playerToRegister;
        private string _playerToGetScore;
        private int _seconds;
        private bool _isSidePanelVisible;

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

        public string GameName
        {
            get { return _dobbleGame.getGameName(); }
        }

        public string GameMode
        {
            get { return _dobbleGame.getNameOfMode() + " " + _dobbleGame.getVersionMode(); }
        }

        public string GameStatus
        {
            get { return _dobbleGame.getStatus(); }
        }

        public string? PlayerTurn
        {
            get { return _dobbleGame.whoseTurnIsIt(); }
        }

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

        public string PlayerToRegister
        {
            get { return _playerToRegister; }
            set
            {
                _playerToRegister = value;
                OnPropertyChanged(nameof(PlayerToRegister));
            }
        }

        public string PlayerToGetScore
        {
            get { return _playerToGetScore; }
            set
            {
                _playerToGetScore = value;
                OnPropertyChanged(nameof(PlayerToGetScore));
            }
        }

        public string Timer
        {
            get
            {
                TimeSpan time = TimeSpan.FromSeconds(_seconds);
                return time.ToString(@"mm\:ss");
            }
        }

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

        public bool IsStartedGame
        {
            get { return _dobbleGame.isStarted(); }
        }

        public bool IsNotStartedGame
        {
            get { return (!IsStartedGame && !IsFinishedGame); }
        }

        public bool IsStartedOrFinishedGame
        {
            get { return IsStartedGame || IsFinishedGame; }
        }

        public bool IsFinishedGame
        {
            get { return _dobbleGame.isFinished(); }
        }

        public bool IsSidePanelVisible
        {
            get { return _isSidePanelVisible; }
            set
            {
                _isSidePanelVisible = value;
                OnPropertyChanged(nameof(IsSidePanelVisible));
            }
        }

        public bool IsCardsInPlay
        {
            get
            {
                return _dobbleGame.getCardsInPlay().Count != 0;
            }
        }

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

        public ICommand ShowPanelCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(ShowPanel));
            }
        }

        public void ShowPanel(object? o)
        {
            IsSidePanelVisible = true;
        }

        public ICommand HidePanelCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(HidePanel));
            }
        }

        public void HidePanel(object? o)
        {
            IsSidePanelVisible = false;
        }

        public ICommand RegisteredPlayersCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(RegisteredPlayers));
            }
        }

        public ICommand NavigateBackCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(NavigateBack));
            }
        }

        public void NavigateBack(object? o)
        {
            _navigationStore.CurrentViewModel = new CreatedGamesViewModel(_navigationStore, _dobbleGamesSet);
        }

        public void RegisteredPlayers(object o)
        {
            MessageBox.Show("Jugadores Registrados:\n" + _dobbleGame.registeredPlayers());
        }

        public ICommand RegisterPlayerCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(RegisterPlayer));
            }
        }

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

        public ICommand GeneralGameInformationCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(GeneralGameInformation));
            }
        }

        public void GeneralGameInformation(object? o)
        {
            MessageBox.Show(_dobbleGame.ToString());
        }

        public ICommand GetPlayerScoreCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(GetPlayerScore));
            }
        }

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

        public ICommand StartGameCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(StartGame));
            }
        }

        public void StartGame(object? o)
        {
            try
            {
                _dobbleGame.start();
                updateDobbleGameInfo();
                IsSidePanelVisible = false;
            }
            catch (DobbleGameException e)
            {
                MessageBox.Show("Error " + e.Code + ": " + e.Message);
            }
        }

        public ICommand FinishGameCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(FinishGame));
            }
        }

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

        public ICommand GameResultsCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(GameResults));
            }
        }

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

        public ICommand PlayCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(Play));
            }
        }

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

        public ICommand PlayElementCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(PlayElement));
            }
        }

        public void PlayElement(object o)
        {
            string element = (string)o;
            string[] data = {element};
            _dobbleGame.play("Elegir elemento en comun", data);
            updateDobbleGameInfo();
        }

        public void updateTimer(object? sender, EventArgs? e)
        {
            _seconds -= 1;
            if (!IsCardsInPlay)
            {
                _timer.Stop();
                _seconds = _dobbleGamesSet.getSeconds(_dobbleGameIndex);
            }

            if (_seconds == 0)
            {
                _timer.Stop();
                _seconds = _dobbleGamesSet.getSeconds(_dobbleGameIndex);
                Play("Pasar");
            }

            OnPropertyChanged(nameof(Timer));
        }
    }
}
