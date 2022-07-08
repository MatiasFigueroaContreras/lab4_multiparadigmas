using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_multiparadigma.ViewModels
{
    //Clase base que permite manejar eventos principalmente que ocurran entre la Vista y la VistaModelo***
    // estos eventos son escencialmente cambio en propiedades/atributos de la Vista y VistaModelo
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
