using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_multiparadigma.ViewModels
{
    /// <summary>
    /// Clase base que permite crear/disparar eventos que notifican a la vista que una propiedad ha cambiado. 
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Evento a disparar al correspondiente manejador.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// permite crear/disparar eventos que notifican a la vista que una propiedad ha cambiado. 
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad que ha cambiado</param>
        protected void OnPropertyChanged(String propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
