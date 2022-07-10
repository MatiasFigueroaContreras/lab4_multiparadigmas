using System;
using System.Collections.Generic;

namespace model.DobbleGameSpace
{
    /**
     * Busca Representar un mazo de cartas Dobble con elementos que este contiene, y
     *  respetando las propiedades de este.
     * @author Matias Figueroa Contreras
     */
    internal class Dobble : IDobble
    {
        /**
        * El mazo de cartas como tal inicializando el mazo en uno vacio.
        */
        private CardsSet dobbleCS = new();

        /**
        * El conjunto de elementos que utilizara/estaran presentes en el mazo de 
        *  cartas.
        */
        private ElementsSet elements;

        /**
        * El numero de elementos con el que se trabajara el mazo de cartas.
        */
        private int numE;

        /**
        *  Lista de las apariciones de los elementos en las cartas, utilizada
        *   para facilitar la creacion parcial del mazo.
        */
        private int[] elementsAppareances;

        /**
        * <p> Constructor, inicializa el numero de elementos que se utilizaran por
        *       carta, sin completar el mazo, ni saber los elementos que este
        *       contendra, creando una lista vacia de estos.
        * </p>
        * @param numE numero de elementos por carta.
        * @return el objeto Dobble creado.
        */
        public Dobble(int numE)
        {
            int n = numE - 1;
            if (isPrime(n))
            {
                int totalCards = totalCardsNumElements(numE);
                elements = new ElementsSet();
                elementsAppareances = new int[totalCards];
                initElementsAppearances();
                this.numE = numE;
            }
            else
            {
                throw new DobbleException(400, "Numero de elementos no valido.");
            }
        }

        /**
        * <p> Constructor, inicializa la lista de elementos y el numero de elemntos
        *       por carta, sin completar el mazo.
        * </p>
        * @param elements elementos con los que se completara el mazo de cartas.
        * @param numE numero de elementos por carta.
        * @return el objeto Dobble creado.
        */
        public Dobble(ElementsSet elements, int numE) : this(numE)
        {
            if(elements.numElements() > totalCardsNumElements(numE))
            {
                this.elements = elements.getRange(1, totalCardsNumElements(numE));
            }
            else
            {
                this.elements = elements;
            }
        }

        /**
        * <p> Constructor, completa el mazo de cartas Dobble segun una lista 
        *       de elementos el numero de elementos por cartas, y el numero de 
        *       cartas a crear.
        * </p>
        * @param elements elementos con los que se completara el mazo de cartas.
        * @param numE numero de elementos por carta.
        * @param maxC maximo numero de cartas que tendra el mazo de cartas.
        * @return el objeto Dobble creado.
        */
        public Dobble(ElementsSet elements, int numE, int maxC) : this(numE)
        {
            int totalCards = totalCardsNumElements(numE);
            if (elements.numElements() < totalCards)
            {
                elements.insertXElements(totalCards - elements.numElements());
                this.elements = elements;
            }
            else if(elements.numElements() > totalCardsNumElements(numE))
            {
                this.elements = elements.getRange(1, totalCardsNumElements(numE));
            }
            else
            {
                this.elements = elements;
            }
            
            initDobbleCards(maxC);
        }

        /**
        * <p> Constructor, completa el mazo de cartas segun una lista de String que
        *       representa los elementos, el numero de elementos por cartas, 
        *       y el numero de cartas a crear.
        * </p>
        * @param elements elementos con los que se completara el mazo de cartas.
        * @param numE numero de elementos por carta.
        * @param maxC maximo numero de cartas que tendra el mazo de cartas.
        * @return el objeto Dobble creado.
        */
        public Dobble(List<string> elements, int numE, int maxC) : this(numE)
        {
            ElementsSet eS = new ElementsSet(elements);
            int totalCards = totalCardsNumElements(numE);
            if (eS.numElements() < totalCards)
            {
                eS.insertXElements(totalCards - eS.numElements());
                this.elements = eS;
            }
            else if(eS.numElements() > totalCardsNumElements(numE))
            {
                this.elements = eS.getRange(1, totalCardsNumElements(numE));
            }
            else
            {
                this.elements = eS;
            }
            initDobbleCards(maxC);
        }

        private bool isPrime(int n)
        {
            if (n == 1)
            {
                return true;
            }
            else if (n <= 0)
            {
                return false;
            }

            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /**
        * <p> Crea el mazo de cartas Dobble con una cantidad maxima de cartas
        *       especificada, esto si existen todos los elementos necesarios.
        * </p>
        * @param maxC maximo numero de cartas que tendra el mazo de cartas.
        */
        public void initDobbleCards(int maxC)
        {
            int totalCards = totalCardsNumElements(numE);
            if (elements.numElements() >= totalCards)
            {
                int n = numE - 1;
                if (maxC <= 0)
                {
                    maxC = totalCardsNumElements(numE);
                }
                dobbleCS.clear();
                firstCardGeneration(n);
                nCardsGeneration(n, maxC - 1);
                n2CardsGeneration(n, maxC - n - 1);
                dobbleCS.mix();
            }
        }

        /**
        * <p> Crea la primera carta del mazo Dobble, y la agrega a este.
        * </p>
        * @param n numero de elementos (this.numE) menos 1.
        */
        private void firstCardGeneration(int n)
        {
            Card card = new Card();
            for (int i = 1; i <= n + 1; i++)
            {
                card.add(elements.nthElement(i));
                elementsAppareances[i - 1]++;
            }
            dobbleCS.add(card);
        }

        /**
        * <p> Crea n cartas del mazo Dobble, y las agrega a este.
        * </p>
        * @param n numero de elementos (this.numE) menos 1.
        * @param maxC maximo de cartas a crear.
        */
        private void nCardsGeneration(int n, int maxC)
        {
            for (int i = 1; i <= n && maxC > 0; i++, maxC--)
            {
                Card card = new Card();
                card.add(elements.nthElement(1));
                elementsAppareances[0]++;
                for (int j = 1; j <= n; j++)
                {
                    card.add(elements.nthElement(n * i + j + 1));
                    elementsAppareances[n * i + j + 1 - 1]++;
                }
                dobbleCS.add(card);
            }
        }

        /**
        * <p> Crea n**2 cartas del mazo Dobble, y las agrega a este.
        * </p>
        * @param n numero de elementos (this.numE) menos 1.
        * @param maxC maximo de cartas a crear.
        */
        private void n2CardsGeneration(int n, int maxC)
        {
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n && maxC > 0; j++, maxC--)
                {
                    Card card = new Card();
                    card.add(elements.nthElement(i + 1));
                    elementsAppareances[i]++;
                    for (int k = 1; k <= n; k++)
                    {
                        card.add(elements.nthElement(n + 2 + n * (k - 1) + ((i - 1) * (k - 1) + j - 1) % n));
                        elementsAppareances[n + 2 + n * (k - 1) + ((i - 1) * (k - 1) + j - 1) % n - 1]++;
                    }
                    dobbleCS.add(card);
                }
            }
        }

        /**
        * <p> Calcula el maximo de cartas que se pueden crear segun el numero de
        *       elementos que estas contienen, es decir el mazo completo Dobble.
        * </p>
        * @param numE numero de elementos por carta.
        */
        public static int totalCardsNumElements(int numE)
        {
            return numE * numE - numE + 1;
        }

        /**
        * <p> Inicializa el numero de apariciones de los elementos en 0.
        * </p>
        */
        private void initElementsAppearances()
        {
            for (int i = 0; i < elementsAppareances.Length; i++)
            {
                elementsAppareances[i] = 0;
            }
        }

        /**
        * <p> Getter.
        * </p>
        * @return una copia del mazo de cartas Dobble.
        */
        public ICardsSet getDobbleCards()
        {
            CardsSet dobbleCSCopy = new CardsSet();
            dobbleCSCopy.setCards(dobbleCS.getCards());
            return dobbleCSCopy;
        }

        /**
        * <p> Setter, que cambia this.dobbleCS por una copia del nuevo mazo de
        *       cartas verificando que esta cumpla con las caracteristicas de un
        *       mazo Dobble.
        * </p>
        * @param newDobbleCards mazo de cartas a setear.
        */
        public void setDobbleCards(CardsSet newDobbleCards)
        {
            if (completeNumElements())
            {
                int[] copyEA = (int[])elementsAppareances.Clone();
                initElementsAppearances();
                if (isDobbleCards(newDobbleCards))
                {
                    dobbleCS.setCards(newDobbleCards.getCards());
                }
                elementsAppareances = copyEA;
            }
        }

        /**
        * <p> Getter.
        * </p>
        * @return una copia de los elementos del mazo de cartas.
        */
        public ElementsSet getElements()
        {
            ElementsSet elementsCopy = new();
            elementsCopy.setElements(elements.getElements());
            return elementsCopy;
        }

        /**
        * <p> Setter, que cambia this.elements por una copia del nuevo conjunto de
        *       elementos, para esto se verifica que no existan cartas en el mazo
        *       Dobble, y que la lista de elementos no supere el maximo de elementos
        *       que tendra el mazo.
        * </p>
        * @param newElements conjunto de elementos a setear.
        */
        public void setElements(ElementsSet newElements)
        {
            if (dobbleCS.numCards() == 0)
            {
                if (newElements.numElements() <= totalCardsNumElements(numE))
                {
                    elements.setElements(newElements.getElements());
                }
            }
        }

        /**
        * <p> Consulta si el conjunto de elementos esta completo
        * </p>
        * @return true si el conjunto esta completo, false si no lo esta.
        */
        private bool completeNumElements()
        {
            return elements.numElements() == totalCardsNumElements(numE);
        }

        /**
        * <p> Consulta si el un conjunto de cartas cumple con las caracteristicas
        *       de un mazo Dobble.
        * </p>
        * @param cards conjunto de cartas a verificar.
        * @return true si el conjunto de cartas es Dobble, false si no lo es.
        */
        private bool isDobbleCards(CardsSet cards)
        {
            for (int i = 1; i <= cards.numCards(); i++)
            {
                Card nCard = cards.nthCard(i);
                if (addElementsAppearences(nCard, elementsAppareances))
                {
                    for (int j = i + 1; j <= cards.numCards(); j++)
                    {
                        if (!nCard.oneCommonElement(cards.nthCard(j)))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /**
        * <p> Agrega las apariciones de los elementos de una carta a una lista 
        *       de enteros que contiene las aparaiciones, sin pasarse del maximo
        *       de elementos que pueden aparecer (segun las caracteristicas de un
        *       mazo Dobble).
        * </p>
        * @param card carta de la cual se contaran las apariciones de los elementos.
        * @param elementsA lista con las apariciones de los elementos.
        * @return true si las apariciones cumplieron con las caracteristicas, false
        *           si no las cumplieron.
        */
        private bool addElementsAppearences(Card card, int[] elementsA)
        {
            for (int i = 1; i <= card.numElements(); i++)
            {
                int eIndex = elements.elementIndex(card.nthElement(i)) - 1;
                if (eIndex <= -1 || elementsA[eIndex] + 1 > numE)
                {
                    return false;
                }
                else
                {
                    elementsA[eIndex]++;
                }
            }
            return true;
        }

        /**
        * <p> Resta las apariciones de los elementos de una carta de la lista con
        *       el conteo de las apariciones (this.elementsAppareances).
        * </p>
        * @param card carta de la cual se quitaran las apariciones de los elementos.
        */
        private void resElementsAppearences(Card card)
        {
            for (int i = 1; i <= card.numElements(); i++)
            {
                int eIndex = elements.elementIndex(card.nthElement(i)) - 1;
                elementsAppareances[eIndex]--;
            }
        }

        /**
        * <p> Calcula el total de cartas que contendra un mazo Dobble completo, a
        *       travez de una carta.
        * </p>
        * @param card carta de la cual se obtendra el numero de elementos.
        * @return numero total de cartas de un mazo Dobble completo.
        */
        public int findTotalCards(Card card)
        {
            return totalCardsNumElements(card.numElements());
        }

        /**
        * <p> Calcula el total de elementos que se necesitan para crear un mazo 
        *       Dobble completo, a travez de una carta.
        * </p>
        * @param card carta de la cual se obtendra el numero de elementos.
        * @return numero total de elementos necesarios para crear un mazo Dobble
        *          completo.
        */
        public int requiredElements(Card card)
        {
            return totalCardsNumElements(card.numElements());
        }

        /**
        * <p> Encuentra las cartas faltantes para que el mazo Dobble (this.dobbleCS)
        *       este completo.
        * </p>
        * @return las cartas faltantes para que el mazo Dobble este completo.
        */
        public CardsSet missingCards()
        {
            int numE = dobbleCS.nthCard(1).numElements();
            Dobble fullDobble = new Dobble(elements, numE, 0);
            fullDobble.dobbleCS.subtract(dobbleCS);
            return fullDobble.dobbleCS;
        }

        /**
        * <p> Consulta si una carta es valida para el mazo Dobble (this.dobbleCS)
        * </p>
        * @param card carta a verificar si es valida.
        * @return true si la carta es valida, false sino lo es.
        */
        public bool isValidCard(Card card)
        {
            for (int i = 1; i <= dobbleCS.numCards(); i++)
            {
                if (!dobbleCS.nthCard(i).oneCommonElement(card))
                {
                    return false;
                }
            }
            return true;
        }

        /**
        * <p> Busca la nth Carta del mazo Dobble, partiendo desde 1.
        * </p>
        * @param n indice (nth) a buscar en el mazo.
        * @return la nth carta buscada.
        */
        public Card nthCard(int i)
        {
            return dobbleCS.nthCard(i);
        }

        /**
        * <p> Cantidad de cartas que tiene el mazo Dobble.
        * </p>
        * @return cantidad de cartas del mazo Dobble.
        */
        public int numCards()
        {
            return dobbleCS.numCards();
        }

        /**
        * <p> A�ade una carta al mazo, respetando que este cumpla con las 
        *       caracteristicas de un mazo Dobble.
        * </p>
        * @param card carta a agregar al mazo Dobble.
        */
        public void addCard(Card card)
        {
            if (completeNumElements())
            {
                int[] copyEA = (int[])elementsAppareances.Clone();
                if (addElementsAppearences(card, copyEA))
                {
                    if (isValidCard(card))
                    {
                        dobbleCS.add(card);
                        elementsAppareances = copyEA;
                    }
                }
            }
        }

        /**
        * <p> Elimina la nth Carta del mazo Dobble y por lo tanto resta las
        *       apariciones de esta.
        * </p>
        * @param n nth carta a eliminar del mazo.
        */
        public void removeCard(int n)
        {
            resElementsAppearences(dobbleCS.nthCard(n));
            dobbleCS.remove(n);
        }

        /**
        * <p> Elimina la Carta del mazo Dobble si es que forma parte de este,
        *       y si lo logra resta las apariciones de esta.
        * </p>
        * @param card carta a eliminar del mazo.
        */
        public void removeCard(Card card)
        {
            dobbleCS.remove(card);
            resElementsAppearences(card);
        }

        /**
        * <p> A�ade un elemento al conjunto de elementos que se utiliza en el mazo
        *       si es que este no esta completo.
        * </p>
        * @param element elemento representado en String a agregar al conjunto.
        */
        public void addElement(string element)
        {
            if (!completeNumElements())
            {
                elements.add(element);
            }
        }

        /**
        * <p> Cuenta la cantidad de elementos del conjunto de estos (this.elements).
        * </p>
        * @return la cantidad de elementos del conjunto.
        */
        public int numElements()
        {
            return elements.numElements();
        }

        /**
        * <p> Busca el nth Elemento en su forma de String del conjunto, 
        *       partiendo desde 1.
        * </p>
        * @param n indice (nth) a buscar en el conjunto.
        * @return el nth elemento buscado en su representacion de String.
        */
        public string nthElement(int i)
        {
            return elements.nthElementString(i);
        }

        /**
        * <p> Elimina el nth Elemento del conjunto (this.elements) (partiendo de 1).
        * </p>
        * @param n nth elemento a eliminar del conjunto (this.elements).
        */
        public void removeElement(int n)
        {
            if (dobbleCS.numCards() == 0)
            {
                elements.remove(n);
            }
        }

        /**
        * <p> Elimina el elemento dado en forma de String si es que se encuentra 
        *       en el conjunto (this.elements).
        * </p>
        * @param element elemento a eliminar del conjunto (this.elements), 
        *           en su forma de String.
        */
        public void removeElement(string e)
        {
            if (dobbleCS.numCards() == 0)
            {
                elements.remove(e);
            }
        }

        /**
        * <p> Pasa la representacion del mazo Dobble a String.
        * </p>
        * @return String en representacion del mazo Dobble, con los elementos que
        *           contiene, y cartas del mazo.
        */
        public override string ToString()
        {
            return "Elements:\n" + elements.ToString() + "\nCards:\n" + dobbleCS.ToString();
        }

        /**
        * <p> Compara this con otro Objeto, para esto compara si son de la misma
        *      clase (Dobble) y luego si los dos conjuntos poseen los mismos valores
        *      de cartas y elementos del mazo.
        * </p>
        * @param object objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public override bool Equals(object? o)
        {
            if (o != null && o.GetType().Equals(GetType()))
            {
                Dobble d = (Dobble)o;
                return d.dobbleCS.Equals(dobbleCS) && d.elements.Equals(elements);
            }
            return false;
        }
    }
}
