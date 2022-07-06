using lab4_multiparadigma.Commands;
using lab4_multiparadigma.Stores;
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

        public InitialViewModel(NavigationStore navigationStore)
        {
            this._navigationStore = navigationStore;
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
            _navigationStore.CurrentViewModel = new CreateGameViewModel(_navigationStore);
        }
    }
}
