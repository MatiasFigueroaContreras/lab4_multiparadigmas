using lab4_multiparadigma.Commands;
using lab4_multiparadigma.Stores;
using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab4_multiparadigma.ViewModels
{
    public class InitialViewModel: ViewModelBase
    {
        public NavigationStore _navigationStore;
        public DobbleGamesSet _dobbleGameSet;

        public InitialViewModel(NavigationStore navigationStore, DobbleGamesSet dobbleGameSet)
        {
            this._navigationStore = navigationStore;
            this._dobbleGameSet = dobbleGameSet;
            
        }

        public ICommand NavigateCreateGameCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(CreateGame));
            }
        }

        public void CreateGame(object? obj)
        {
            _navigationStore.CurrentViewModel = new CreateGameViewModel(_navigationStore, _dobbleGameSet);
        }

        public ICommand NavigateCreatedGamesCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(CreatedGames));
            }
        }

        public void CreatedGames(object? obj)
        {
            _navigationStore.CurrentViewModel = new CreatedGamesViewModel(_navigationStore, _dobbleGameSet);
        }
    }
}
