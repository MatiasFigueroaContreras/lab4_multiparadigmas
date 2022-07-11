using lab4_multiparadigma.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_multiparadigma.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        ///  Almacen de navegacion el cual guarda la vista modelo actual y permite 
        ///     gestionar estas mediante eventos de cambio.
        /// </summary>
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        /// <summary>
        /// Constructor que asigna los valores entregados a los correspondientes atributos y
        ///     suscribe el manejador de eventos OnCurrentViewModelChanged al evento
        ///     CurrentViewModelChanged.
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="dobbleGameSet"></param>
        public MainWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        /// <summary>
        /// Manejador de eventos, que dispara un evento si se cambia el tipo
        ///     de VistaModelo.
        /// </summary>
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel)); // Si se cambia el tipo de VistaModelo, se dispara el evento.
        }
    }
}
