using System;
using System.Collections.Generic;

namespace model.DobbleGameSpace
{
    /**
     * La clase Element, busca representar los elementos que estara presente en las
     *  cartas, de una manera mas estandarizada.
     * @author Matias Figueroa Contreras
     */
    internal class Element : IElement
    {
        /**
        * El Elemento que sera representado como una String.
        */
        public string element;

        /**
        * <p> Crea el objeto Element.
        * </p>
        * @param element El elemento representado en String
        * @return el objeto Element creado
        */
        public Element(string element)
        {
            this.element = element;
        }

        /**
        * <p> Crea el objeto Element, transformando el elemento entero a String.
        * </p>
        * @param element El elemento representado en int
        * @return el objeto Element creado
        */
        public Element(int element)
        {
            this.element = Convert.ToString(element);
        }

        /**
        * <p> Compara this con otro Objeto, para esto compara si son de la misma
        *      clase (Element) y luego si el valor de su atributo es el mismo.
        * </p>
        * @param object objeto a comparar con this
        * @return true si son iguales, false si no son iguales.
        */
        public override bool Equals(object? o)
        {
            if (o != null && o.GetType().Equals(GetType()))
            {
                Element e = (Element)o;
                return element.Equals(e.element);
            }
            return false;
        }

        /**
        * <p> Pasa la representacion del Elemento a String (es el mismo this.element).
        * </p>
        * @return String en representacion del elemento (this.element).
        */
        public override string ToString()
        {
            return element;
        }
    }
}
