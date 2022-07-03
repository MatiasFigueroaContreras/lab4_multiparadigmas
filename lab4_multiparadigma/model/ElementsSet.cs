﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    /**
     * Busca representar un conjunto de elementos (IElement), respetando como 
     *  funcionan los conjuntos.
     * @author Matias Figueroa Contreras
     */
    internal class ElementsSet: IElementsSet
    {
        /**
* El conjunto de elementos implementado como un ArrayList.
*/
        protected List<Element> elementsSet;

        /**
        * <p> Constructor, con un conjunto de elementos vacio.
        * </p>
        * @return el objeto ElementSet creado.
        */
        public ElementsSet()
        {
            this.elementsSet = new();
        }

        /**
        * <p> Constructor, agrega elementos al conjunto
        *       (Respetando que no se repitan estos).
        * </p>
        * @param elements Arreglo de strings con los elementos a agregar.
        * @return el objeto ElementSet creado.
        */
        public ElementsSet(List<String> elements)
        {
            this.elementsSet = new();
            for (int i = 0; i < elements.Count; i++)
            {
                Element e = new Element(elements[i]);
                add(e);
            }
        }

        /**
        * <p> Getter.
        * </p>
        * @return una copia del Arreglo de elementos.
        */
        public List<Element> getElements()
        {
            List<Element> eSCopy = new(this.elementsSet);
            return eSCopy;
        }

        /**
        * <p> Setter, que cambia this.elements por una copia de la nueva lista de 
        *       elementos verificando que esta cumpla con ser un conjunto de 
        *       elementos.
        * </p>
        * @param newElements lista de elementos a agregar.
        */
        public void setElements(List<Element> newElements)
        {
            if (isElementsSet(newElements))
            {
                this.elementsSet = new(newElements);
            }
        }

        /**
        * <p> Busca verificar que una lista de elementos sea un conjunto de estos.
        *       es decir, sin elementos repetidos.
        * </p>
        * @param elements Arreglo de elementos a verificar.
        * @return true si es un conjunto, false si no es un conjunto.
        */
        private bool isElementsSet(List<Element> elements)
        {
            ElementsSet eSCo = new ElementsSet();
            for (int i = 0; i < elements.Count; i++)
            {
                eSCo.add(elements[i]);
            }
            return eSCo.numElements() == elements.Count;
        }

        /**
        * <p> Busca el nth Elemento del conjunto, partiendo desde 1.
        * </p>
        * @param n indice (nth) a buscar en el conjunto.
        * @return el nth elemento buscado.
        */
        public Element nthElement(int n)
        {
            return this.elementsSet[n - 1];
        }


        /**
        * <p> Busca el nth Elemento del conjunto y lo representa en string.
        * </p>
        * @param n indice (nth) a buscar en el conjunto.
        * @return el nth elemento representado en string.
        */
        public String nthElementString(int n)
        {
            return nthElement(n).ToString();
        }


        /**
        * <p> Cuenta la cantidad de elementos en el conjunto.
        * </p>
        * @return la cantidad de elementos del conjunto.
        */
        public int numElements()
        {
            return this.elementsSet.Count;
        }


        /**
        * <p> A�ade un elemento al conjunto, respetando que este no sea parte de 
        * este.
        * </p>
        * @param element elemento a agregar al conjunto.
        */
        public void add(Element element)
        {
            if (!contains(element))
            {
                this.elementsSet.Add(element);
            }
        }

        /**
        * <p> A�ade un elemento al conjunto, respetando que este no sea parte de
        *       ya, pasando el string a su representacion en Element.
        * </p>
        * @param element elemento a agregar al conjunto.
        */
        public void add(String element)
        {
            Element el = new(element);
            add(el);
        }

        /**
        * <p> Agrega x cantidad de elementos al conjunto
        * </p>
        * @param x cantidad de elementos a agregar.
        */
        public void insertXElements(int x)
        {
            for (int i = 1; i <= x; i++)
            {
                Element eX = new(i);
                if (contains(eX))
                {
                    x++;
                }
                else
                {
                    add(eX);
                }
            }
        }

        /**
        * <p> Elimina el nth Elemento del conjunto (partiendo de 1).
        * </p>
        * @param n nth elemento a eliminar del conjunto.
        */
        public void remove(int n)
        {
            this.elementsSet.RemoveAt(n - 1);
        }

        /**
        * <p> Elimina el elemento dado si es que se encuentra en el conjunto.
        * </p>
        * @param element elemento a eliminar del conjunto.
        */
        public void remove(Element element)
        {
            this.elementsSet.Remove(element);
        }

        /**
        * <p> Elimina el elemento dado en forma de String si es que se encuentra 
        *       en el conjunto.
        * </p>
        * @param element elemento a eliminar del conjunto, en su forma de String.
        */
        public void remove(String element)
        {
            Element el = new(element);
            remove(el);
        }

        /**
        * <p> Elimina todos los elementos del conjunto.
        * </p>
        */
        public void clear()
        {
            this.elementsSet.Clear();
        }

        /**
        * <p> Verifica si un elmento pertenece al conjunto.
        * </p>
        * @param element elemento a verifcar.
        * @return true si el conjunto contiene el elemento dado, false sino lo
        *           contiene.
        */
        public bool contains(Element element)
        {
            for (int i = 1; i <= numElements(); i++)
            {
                if (element.Equals(nthElement(i)))
                {
                    return true;
                }
            }
            return false;
        }

        /**
        * <p> Verifica si un elmento pertenece al conjunto, transformando el string
        *       a su representacion en elemento.
        * </p>
        * @param element representacion en string del elemento a verifcar.
        * @return true si el conjunto contiene el elemento dado, false sino lo
        *           contiene.
        */
        public bool contains(String element)
        {
            Element e = new(element);
            for (int i = 1; i <= numElements(); i++)
            {
                if (e.Equals(nthElement(i)))
                {
                    return true;
                }
            }
            return false;
        }

        /**
        * <p> Busca el indice del elemento entregado.
        * </p>
        * @param element elemento a buscar el indice.
        * @return el indice del elemento, y -1 si no lo encuentra.
        */
        public int elementIndex(Element element)
        {
            for (int i = 1; i <= numElements(); i++)
            {
                if (element.Equals(nthElement(i)))
                {
                    return i;
                }
            }
            return -1;
        }

        /**
        * <p> Pasa la representacion del Conjunto de Elementos a String.
        * </p>
        * @return String en representacion del conjunto de elementos.
        */
        public override String ToString()
        {
            String str = "";
            int i;
            for (i = 1; i < numElements(); i++)
            {
                str = str + nthElement(i).ToString() + ", ";
            }
            if (i != 1)
            {
                return str + nthElement(i);
            }
            else
            {
                return str;
            }
        }


        /**
        * <p> Compara this con otro Objeto, para esto compara si son de la misma
        *      clase (ElementSet) y luego si los dos conjuntos poseen los mismos
        *      elementos.
        * </p>
        * @param object objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public override bool Equals(Object? o)
        {
            if (o != null && o.GetType().Equals(this.GetType()))
            {
                ElementsSet eS = (ElementsSet)o;
                if (numElements() == eS.numElements())
                {
                    for (int i = 1; i <= eS.numElements(); i++)
                    {
                        if (!contains(eS.nthElement(i)))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            return false;
        }
    }
}
