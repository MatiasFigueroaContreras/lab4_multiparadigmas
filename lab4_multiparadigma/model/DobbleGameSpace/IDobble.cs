using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model.DobbleGameSpace
{
    internal interface IDobble
    {
        /**
        * <p> Calcula el total de cartas que contendra un mazo Dobble completo, a
        *       travez de una carta.
        * </p>
        * @param card carta de la cual se obtendra el numero de elementos.
        * @return numero total de cartas de un mazo Dobble completo.
        */
        public int findTotalCards(Card card);

        /**
        * <p> Calcula el total de elementos que se necesitan para crear un mazo 
        *       Dobble completo, a travez de una carta.
        * </p>
        * @param card carta de la cual se obtendra el numero de elementos.
        * @return numero total de elementos necesarios para crear un mazo Dobble
        *          completo.
        */
        public int requiredElements(Card card);

        /**
        * <p> Encuentra las cartas faltantes para que el mazo Dobble este completo.
        * </p>
        * @return las cartas faltantes para que el mazo Dobble este completo.
        */
        public CardsSet missingCards();

        /**
        * <p> Consulta si una carta es valida para el mazo Dobble
        * </p>
        * @param card carta a verificar si es valida.
        * @return true si la carta es valida, false sino lo es.
        */
        public bool isValidCard(Card card);

        /**
        * <p> Busca la nth Carta del mazo Dobble.
        * </p>
        * @param n indice (nth) a buscar en el mazo.
        * @return la nth carta buscada.
        */
        public Card nthCard(int n);

        /**
        * <p> Cantidad de cartas que tiene el mazo Dobble.
        * </p>
        * @return cantidad de cartas del mazo Dobble.
        */
        public int numCards();

        /**
        * <p> A�ade una carta al mazo, respetando que este cumpla con las 
        *       caracteristicas de un mazo Dobble.
        * </p>
        * @param card carta a agregar al mazo Dobble.
        */
        public void addCard(Card card);

        /**
        * <p> Elimina la nth Carta del mazo Dobble y por lo tanto resta las
        *       apariciones de esta.
        * </p>
        * @param n nth carta a eliminar del mazo.
        */
        public void removeCard(int n);

        /**
        * <p> Elimina la Carta del mazo Dobble si es que forma parte de este,
        *       y si lo logra resta las apariciones de esta.
        * </p>
        * @param card carta a eliminar del mazo.
        */
        public void removeCard(Card card);

        /**
        * <p> A�ade un elemento al conjunto de elementos que se utiliza en el mazo
        *       si es que este no esta completo.
        * </p>
        * @param element elemento representado en String a agregar al conjunto.
        */
        public void addElement(string element);

        /**
        * <p> Cuenta la cantidad de elementos del conjunto de estos.
        * </p>
        * @return la cantidad de elementos del conjunto.
        */
        public int numElements();

        /**
        * <p> Busca el nth Elemento en su forma de String del conjunto, 
        *       partiendo desde 1.
        * </p>
        * @param n indice (nth) a buscar en el conjunto.
        * @return el nth elemento buscado en su representacion de String.
        */
        public string nthElement(int i);

        /**
        * <p> Elimina el nth Elemento del conjunto (this.elements) (partiendo de 1).
        * </p>
        * @param n nth elemento a eliminar del conjunto (this.elements).
        */
        public void removeElement(int n);

        /**
        * <p> Elimina el elemento dado en forma de String si es que se encuentra 
        *       en el conjunto (this.elements).
        * </p>
        * @param element elemento a eliminar del conjunto (this.elements), 
        *           en su forma de String.
        */
        public void removeElement(string element);

        /**
        * <p> Getter.
        * </p>
        * @return mazo de cartas Dobble.
        */
        public ICardsSet getDobbleCards();

        /**
        * <p> Setter.
        * </p>
        * @param cards mazo de cartas a setear.
        */
        public void setDobbleCards(CardsSet cards);

        /**
        * <p> Getter.
        * </p>
        * @return elementos del mazo de cartas.
        */
        public ElementsSet getElements();

        /**
        * <p> Setter.
        * </p>
        * @param elements conjunto de elementos a setear.
        */
        public void setElements(ElementsSet elements);

        /**
        * <p> Pasa la representacion del mazo Dobble a String.
        * </p>
        * @return String en representacion del mazo Dobble, con los elementos que
        *           contiene, y cartas del mazo.
        */
        public string ToString();

        /**
        * <p> Compara this con otro Objeto.
        * </p>
        * @param object objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public bool Equals(object? o);
    }
}
