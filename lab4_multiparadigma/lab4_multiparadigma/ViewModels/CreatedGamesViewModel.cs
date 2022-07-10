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

namespace lab4_multiparadigma.ViewModels
{
    public class CreatedGamesViewModel : ViewModelBase
    {
        public NavigationStore _navigationStore;
        private DobbleGamesSet _dobbleGamesSet;
        public CreatedGamesViewModel(NavigationStore navigationStore, DobbleGamesSet dobbleGamesSet)
        {
            this._navigationStore = navigationStore;
            this._dobbleGamesSet = dobbleGamesSet;
        }

        public List<DobbleGame> DobbleGamesSet
        {
            get { return _dobbleGamesSet.games; }
        }

        public ICommand NavigateDobbleGameCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(NavigateDobbleGame));
            }
        }

        public void NavigateDobbleGame(object dobbleGame)
        {
            DobbleGame dG = (DobbleGame)dobbleGame;
            _navigationStore.CurrentViewModel = new DobbleGameViewModel(_navigationStore, _dobbleGamesSet, _dobbleGamesSet.getGameIndex(dG));
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
            _navigationStore.CurrentViewModel = new InitialViewModel(_navigationStore, _dobbleGamesSet);
        }
    }
}
