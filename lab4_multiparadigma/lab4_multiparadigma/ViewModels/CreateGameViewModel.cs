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
    public class CreateGameViewModel: ViewModelBase
    {
        public NavigationStore _navigationStore;
        public CreateGameViewModel(NavigationStore navigationStore)
        {
            this._navigationStore = navigationStore;
        }

        public ICommand NavigateInitialCommand
        {
            get
            {
                return new RelayCommand(new Action<object>(Initial));
            }
        }

        public void Initial(object? obj)
        {
            _navigationStore.CurrentViewModel = new InitialViewModel(_navigationStore);
        }
    }
}
