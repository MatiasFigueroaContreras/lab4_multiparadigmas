using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    /**
     * Interfaz que busca estandarizar y definir las acciones que tiene que realizar
     *  un elemento
     * @author Matias Figueroa Contreras
     */
    internal interface IElement
    {
        /**
        * <p> Compara si son iguales this con otro Objeto.
        * </p>
        * @param obj objeto a comparar con this
        * @return true si son iguales, false si no son iguales.
        */
        public bool Equals(Object? obj);
        /**
        * <p> Pasa la representacion del elemento a String.
        * </p>
        * @return String en representacion del elemento.
        */
        public String ToString();
    }
}
