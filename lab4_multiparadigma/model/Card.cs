using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    /**
     * Representa una carta para el juego Dobble, la cual hereda los atributos
     *  y metodos de la clase ElementsSet.
     * @author Matias Figueroa Contreras
     */
    internal class Card: ElementsSet
    {
        /**
        * <p> Constructor, con un conjunto de elementos vacio.
        * </p>
        * @return el objeto Card creado.
        */
        public Card()
        {
            this.elementsSet = new();
        }

        /**
        * <p> Constructor, agrega elementos al conjunto
        *       (Respetando que no se repitan estos).
        * </p>
        * @param elements Arreglo de strings con los elementos a agregar.
        * @return el objeto Card creado.
        */
        public Card(List<String> elements)
        {
            this.elementsSet = new();
            for (int i = 0; i < elements.Count; i++)
            {
                Element e = new Element(elements[i]);
                add(e);
            }
        }

        /**
        * <p> Cuenta los elmentos comunes entre la carta entregada y this. 
        * </p>
        * @param card carta con la cual se comparan los elementos.
        * @return numero de elementos comunes.
        */
        public int commonElements(Card card)
        {
            int cont = 0;
            for (int i = 1; i <= card.numElements(); i++)
            {
                if (contains(card.nthElement(i)))
                {
                    cont++;
                }
            }
            return cont;
        }

        /**
        * <p> Consulta si una carta dada tiene solo un elemento en comun con this.
        * </p>
        * @param card carta con la cual se comparan los elementos.
        * @return true si tiene un solo elemento en comun, false sino.
        */
        public bool oneCommonElement(Card card)
        {
            int commonE = commonElements(card);
            return commonE == 1;
        }
    }
}
