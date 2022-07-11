using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace lab4_multiparadigma.Resources.Helpers
{
    /// <summary>
    /// (Recurso para la vista) Esta clase es la encargada de convertir el 
    ///     nombre de una imagen en un BitmapImage.
    /// </summary>
    public class NameToBitmapImageConverter : IValueConverter
    {
        /// <summary>
        /// Convierte el nombre de una imagen en un BitmapImage enlazando el nombre
        ///     hacia la direccion en que se encuentra las imagenes
        /// </summary>
        /// <param name="value">valor a covertir</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string element = (string)value;
            return new BitmapImage(new Uri(string.Format("../Resources/Images/Elements/{0}.png", element), UriKind.Relative));
        }

        /// <summary>
        /// No implementado ya que no es ocupado, pero es pedido por el IValueConverter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
