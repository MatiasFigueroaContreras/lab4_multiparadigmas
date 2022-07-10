using lab4_multiparadigma.Commands;
using lab4_multiparadigma.Resources.Helpers;
using lab4_multiparadigma.Stores;
using model;
using model.DobbleGameSpace;
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
        public NavigationStore _navigationStore;
        public DobbleGamesSet _dobbleGamesSet;
        private String _gameName;
        private int _numberCards;
        private String _gameMode;
        private int _numberElements;
        private int _gameTime;
        private int _maxPlayers;

        public CreateGameViewModel(NavigationStore navigationStore, DobbleGamesSet dobbleGamesSet)
        {
            this._navigationStore = navigationStore;
            this._dobbleGamesSet = dobbleGamesSet;
        }

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
                        //Marcar disparando un evento en la vista que se tiene que ingresar un valor entero
                    }
                }
            }
        }

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
                        //Marcar disparando un evento en la vista que se tiene que ingresar un valor entero
                    }
                }
            }
        }

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
                        //Marcar disparando un evento en la vista que se tiene que ingresar un valor entero
                    }
                }
            }
        }

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
                        //Marcar disparando un evento en la vista que se tiene que ingresar un valor entero
                    }
                }
            }
        }

        public ICommand AddCardCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(AddCard));
            }
        }

        public void AddCard(object? o)
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
            catch(DobbleGameException e)
            {
                //Mensaje con error
                MessageBox.Show("Error " + e.Code + ": " + e.Message);
            }

        }

        public ICommand NavigateBackCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(NavigateBack));
            }
        }

        public void NavigateBack(object? obj)
        {
            _navigationStore.CurrentViewModel = new InitialViewModel(_navigationStore, _dobbleGamesSet);
        }

    }
}
