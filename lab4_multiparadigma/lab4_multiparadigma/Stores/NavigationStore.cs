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
        /// <summary>
        /// Atributo (evento) que permite suscribirse a eventos
        /// </summary>
        public event Action CurrentViewModelChanged;

        /// <summary>
        /// Vista Modelo actual
        /// </summary>
        private ViewModelBase _currentViewModel;

        /// <summary>
        /// Vista modelo actual a obtener y modificar, en donde si
        ///     se modifica dispara el evento al manejador de eventos.
        /// </summary>
        public ViewModelBase CurrentViewModel { 
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged(); // Si ocurre el evento en el que cambian el valor de la vista actual se dispara este al manejador de eventos
            }
        }

        /// <summary>
        /// Invoca los manejadores de eventos suscritos (si es que se suscribieron).
        /// </summary>
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
