using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    /**
     * 
     * @author Matias Figueroa Contreras
     */
    internal interface IElementsSet
    {
        /**
        * <p> Getter.
        * </p>
        * @return el arreglo de elementos.
        */
        public List<Element> getElements();

        /**
        * <p> Setter.
        * </p>
        * @param elements lista de elementos a setear.
        */
        public void setElements(List<Element> elements);

        /**
        * <p> Busca el nth Elemento del conjunto.
        * </p>
        * @param n indice (nth) a buscar en el conjunto.
        * @return el nth elemento buscado.
        */
        public Element nthElement(int n);

        /**
        * <p> Busca el nth Elemento del conjunto y lo representa en string.
        * </p>
        * @param n indice (nth) a buscar en el conjunto.
        * @return el nth elemento representado en string.
        */
        public String nthElementString(int n);

        /**
        * <p> Cuenta la cantidad de elementos en el conjunto.
        * </p>
        * @return la cantidad de elementos del conjunto.
        */
        public int numElements();

        /**
        * <p> A�ade un elemento al conjunto, debe respetar que no este presente ya.
        * </p>
        * @param element elemento a agregar al conjunto.
        */
        public void add(Element element);

        /**
        * <p> A�ade un elemento al conjunto, respetando que este no sea parte de
        *       ya, pasando el string a su representacion en Element.
        * </p>
        * @param element elemento a agregar al conjunto.
        */
        public void add(String element);

        /**
        * <p> Agrega x cantidad de elementos al conjunto
        * </p>
        * @param x cantidad de elementos a agregar.
        */
        public void insertXElements(int x);

        /**
        * <p> Elimina el nth Elemento del conjunto.
        * </p>
        * @param n nth elemento a eliminar del conjunto.
        */
        public void remove(int n);

        /**
        * <p> Elimina el elemento dado si es que se encuentra en el conjunto.
        * </p>
        * @param element elemento a eliminar del conjunto.
        */
        public void remove(Element element);

        /**
        * <p> Elimina el elemento dado en forma de String si es que se encuentra 
        *       en el conjunto.
        * </p>
        * @param element elemento a eliminar del conjunto, en su forma de String.
        */
        public void remove(String element);

        /**
        * <p> Elimina todos los elementos del conjunto.
        * </p>
        */
        public void clear();

        /**
        * <p> Verifica si un elmento pertenece al conjunto.
        * </p>
        * @param element elemento a verifcar.
        * @return true si el conjunto contiene el elemento dado, false sino lo
        *           contiene.
        */
        public bool contains(Element element);

        /**
        * <p> Verifica si un elmento pertenece al conjunto, transformando el string
        *       a su representacion en elemento.
        * </p>
        * @param element representacion en string del elemento a verifcar.
        * @return true si el conjunto contiene el elemento dado, false sino lo
        *           contiene.
        */
        public bool contains(String element);

        /**
        * <p> Busca el indice del elemento entregado.
        * </p>
        * @param element elemento a buscar el indice.
        * @return el indice del elemento, y -1 si no lo encuentra.
        */
        public int elementIndex(Element element);

        /**
        * <p> Pasa la representacion del Conjunto de Elementos a String.
        * </p>
        * @return String en representacion del conjunto de elementos.
        */
        public String ToString();

        /**
        * <p> Compara this con otro Objeto.
        * </p>
        * @param obj objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public bool Equals(Object? obj);
    }
}
