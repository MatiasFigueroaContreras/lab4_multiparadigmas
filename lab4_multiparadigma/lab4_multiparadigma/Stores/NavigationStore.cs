using lab4_multiparadigma.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab4_multiparadigma.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged; // Atributo que permite suscribirse a eventos
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel { 
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged(); // Si ocurre el evento en el que cambian el valor de la vista actual se dispara este al manejador de eventos
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke(); // Invoca los manejadores de eventos suscritos (si es que se suscribio almenos uno)
        }
    }
}
