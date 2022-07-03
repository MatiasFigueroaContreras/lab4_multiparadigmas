using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    /**
     * Busca Representar un mazo de cartas Dobble con elementos que este contiene, y
     *  respetando las propiedades de este.
     * @author Matias Figueroa Contreras
     */
    internal class Dobble: IDobble
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
                this.elements = new ElementsSet();
                this.elementsAppareances = new int[totalCards];
                initElementsAppearances();
                this.numE = numE;
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
        public Dobble(ElementsSet elements, int numE): this(numE)
        {
            this.elements = elements;
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
        public Dobble(ElementsSet elements, int numE, int maxC): this(numE)
        {
            int totalCards = totalCardsNumElements(numE);
            if (elements.numElements() < totalCards)
            {
                elements.insertXElements(totalCards - elements.numElements());
            }
            this.elements = elements;
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
        public Dobble(List<String> elements, int numE, int maxC): this(numE)
        {
            ElementsSet eS = new ElementsSet(elements);
            int totalCards = totalCardsNumElements(numE);
            if (eS.numElements() < totalCards)
            {
                eS.insertXElements(totalCards - eS.numElements());
            }
            this.elements = eS;
            initDobbleCards(maxC);
        }

        private bool isPrime(int n)
        {
            if(n == 1)
            {
                return true;
            }
            else if (n <= 0)
            {
                return false;
            }

            for(int i = 2; i < n; i++)
            {
                if(n % i == 0)
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
            int totalCards = totalCardsNumElements(this.numE);
            if (this.elements.numElements() >= totalCards)
            {
                int n = this.numE - 1;
                if (maxC <= 0)
                {
                    maxC = totalCardsNumElements(numE);
                }
                this.dobbleCS.clear();
                firstCardGeneration(n);
                nCardsGeneration(n, maxC - 1);
                n2CardsGeneration(n, maxC - n - 1);
                this.dobbleCS.mix();
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
                card.add(this.elements.nthElement(i));
                this.elementsAppareances[i - 1]++;
            }
            this.dobbleCS.add(card);
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
                card.add(this.elements.nthElement(1));
                this.elementsAppareances[0]++;
                for (int j = 1; j <= n; j++)
                {
                    card.add(this.elements.nthElement(n * i + (j + 1)));
                    this.elementsAppareances[(n * i + (j + 1)) - 1]++;
                }
                this.dobbleCS.add(card);
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
                    card.add(this.elements.nthElement(i + 1));
                    this.elementsAppareances[i]++;
                    for (int k = 1; k <= n; k++)
                    {
                        card.add(this.elements.nthElement(n + 2 + n * (k - 1) + (((i - 1) * (k - 1) + j - 1) % n)));
                        this.elementsAppareances[(n + 2 + n * (k - 1) + (((i - 1) * (k - 1) + j - 1) % n)) - 1]++;
                    }
                    this.dobbleCS.add(card);
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
            for (int i = 0; i < this.elementsAppareances.Length; i++)
            {
                this.elementsAppareances[i] = 0;
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
            dobbleCSCopy.setCards(this.dobbleCS.getCards());
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
                int[] copyEA = (int[])this.elementsAppareances.Clone();
                initElementsAppearances();
                if (isDobbleCards(newDobbleCards))
                {
                    this.dobbleCS.setCards(newDobbleCards.getCards());
                }
                this.elementsAppareances = copyEA;
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
            elementsCopy.setElements(this.elements.getElements());
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
            if (this.dobbleCS.numCards() == 0)
            {
                if (newElements.numElements() <= totalCardsNumElements(numE))
                {
                    this.elements.setElements(newElements.getElements());
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
            return this.elements.numElements() == totalCardsNumElements(numE);
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
                if (addElementsAppearences(nCard, this.elementsAppareances))
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
                int eIndex = this.elements.elementIndex(card.nthElement(i)) - 1;
                if (eIndex <= -1 || (elementsA[eIndex] + 1) > this.numE)
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
                int eIndex = this.elements.elementIndex(card.nthElement(i)) - 1;
                this.elementsAppareances[eIndex]--;
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
            int numE = this.dobbleCS.nthCard(1).numElements();
            Dobble fullDobble = new Dobble(this.elements, numE, 0);
            fullDobble.dobbleCS.subtract(this.dobbleCS);
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
            for (int i = 1; i <= this.dobbleCS.numCards(); i++)
            {
                if (!this.dobbleCS.nthCard(i).oneCommonElement(card))
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
            return this.dobbleCS.nthCard(i);
        }

        /**
        * <p> Cantidad de cartas que tiene el mazo Dobble.
        * </p>
        * @return cantidad de cartas del mazo Dobble.
        */
        public int numCards()
        {
            return this.dobbleCS.numCards();
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
                int[] copyEA = (int[])this.elementsAppareances.Clone();
                if (addElementsAppearences(card, copyEA))
                {
                    if (isValidCard(card))
                    {
                        this.dobbleCS.add(card);
                        this.elementsAppareances = copyEA;
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
            resElementsAppearences(this.dobbleCS.nthCard(n));
            this.dobbleCS.remove(n);
        }

        /**
        * <p> Elimina la Carta del mazo Dobble si es que forma parte de este,
        *       y si lo logra resta las apariciones de esta.
        * </p>
        * @param card carta a eliminar del mazo.
        */
        public void removeCard(Card card)
        {
            this.dobbleCS.remove(card);
            resElementsAppearences(card);
        }

        /**
        * <p> A�ade un elemento al conjunto de elementos que se utiliza en el mazo
        *       si es que este no esta completo.
        * </p>
        * @param element elemento representado en String a agregar al conjunto.
        */
        public void addElement(String element)
        {
            if (!completeNumElements())
            {
                this.elements.add(element);
            }
        }

        /**
        * <p> Cuenta la cantidad de elementos del conjunto de estos (this.elements).
        * </p>
        * @return la cantidad de elementos del conjunto.
        */
        public int numElements()
        {
            return this.elements.numElements();
        }

        /**
        * <p> Busca el nth Elemento en su forma de String del conjunto, 
        *       partiendo desde 1.
        * </p>
        * @param n indice (nth) a buscar en el conjunto.
        * @return el nth elemento buscado en su representacion de String.
        */
        public String nthElement(int i)
        {
            return this.elements.nthElementString(i);
        }

        /**
        * <p> Elimina el nth Elemento del conjunto (this.elements) (partiendo de 1).
        * </p>
        * @param n nth elemento a eliminar del conjunto (this.elements).
        */
        public void removeElement(int n)
        {
            if (this.dobbleCS.numCards() == 0)
            {
                this.elements.remove(n);
            }
        }

        /**
        * <p> Elimina el elemento dado en forma de String si es que se encuentra 
        *       en el conjunto (this.elements).
        * </p>
        * @param element elemento a eliminar del conjunto (this.elements), 
        *           en su forma de String.
        */
        public void removeElement(String e)
        {
            if (this.dobbleCS.numCards() == 0)
            {
                this.elements.remove(e);
            }
        }

        /**
        * <p> Pasa la representacion del mazo Dobble a String.
        * </p>
        * @return String en representacion del mazo Dobble, con los elementos que
        *           contiene, y cartas del mazo.
        */
        public override String ToString()
        {
            return "Elements:\n" + this.elements.ToString() + "\nCards:\n" + this.dobbleCS.ToString();
        }

        /**
        * <p> Compara this con otro Objeto, para esto compara si son de la misma
        *      clase (Dobble) y luego si los dos conjuntos poseen los mismos valores
        *      de cartas y elementos del mazo.
        * </p>
        * @param object objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public override bool Equals(Object? o)
        {
            if (o != null && o.GetType().Equals(this.GetType()))
            {
                Dobble d = (Dobble)o;
                return d.dobbleCS.Equals(this.dobbleCS) && d.elements.Equals(this.elements);
            }
            return false;
        }
    }
}
