using System;
using System.Collections.Generic;

namespace model.DobbleGameSpace
{
    /**
     * Busca representar un conjunto de cartas (ICardsSet), respetando como
     *  funcionan los conjuntos.
     * @author Matias Figueroa Contreras
     */
    internal class CardsSet : ICardsSet
    {
        /**
        * El conjunto de cartas representado en un Arreglo. Creado en primera
        *   instancia vacio.
        */
        private List<Card> cardsSet = new();

        /**
        * <p> Getter.
        * </p>
        * @return una copia del Arreglo de cartas.
        */
        public List<Card> getCards()
        {
            List<Card> cSCopy = new(cardsSet);
            return cSCopy;
        }

        /**
        * <p> Setter, que cambia this.cardsSet por una copia de la nueva lista de 
        *       cartas verificando que esta cumpla con ser un conjunto de 
        *       cartas.
        * </p>
        * @param newCards lista de cartas a setear.
        */
        public void setCards(List<Card> newCards)
        {
            if (isCardsSet(newCards))
            {
                cardsSet = new(newCards);
            }
        }

        /**
        * <p> Busca verificar que una lista de cartas sea un conjunto de estos.
        *       es decir, sin cartas repetidas.
        * </p>
        * @param cards Arreglo de cartas a verificar.
        * @return true si es un conjunto, false si no es un conjunto.
        */
        private bool isCardsSet(List<Card> cards)
        {
            CardsSet cSCo = new CardsSet();
            for (int i = 0; i < cards.Count; i++)
            {
                cSCo.add(cards[i]);
            }
            return cSCo.numCards() == cards.Count;
        }

        /**
        * <p> A�ade una carta al conjunto, respetando que este no sea parte de este.
        * </p>
        * @param card carta a agregar al conjunto
        */
        public void add(Card card)
        {
            if (!contains(card))
            {
                cardsSet.Add(card);
            }
        }

        /**
        * <p> Busca la nth Carta del conjunto, partiendo desde 1.
        * </p>
        * @param n indice (nth) a buscar en el conjunto.
        * @return la nth carta buscada.
        */
        public Card nthCard(int n)
        {
            return cardsSet[n - 1];
        }

        /**
       * <p> Cuenta la cantidad de cartas en el conjunto.
       * </p>
       * @return cantidad de cartas en el conjunto.
       */
        public int numCards()
        {
            return cardsSet.Count;
        }

        /**
        * <p> Une un conjunto de cartas dado con this, respetando la union
        *       de conjuntos conocida.
        * </p>
        * @param cards cartas a unir con this.
        */
        public void union(ICardsSet cards)
        {
            for (int i = 1; i <= cards.numCards(); i++)
            {
                add(cards.nthCard(i));
            }
        }

        /**
        * <p> Hace una resta de conjuntos entre las cartas dadas y this, respetando
        *       la reste de conjuntos conocidas.
        * </p>
        * @param cards cartas a restar con this.
        */
        public void subtract(ICardsSet cards)
        {
            for (int i = 1; i <= cards.numCards(); i++)
            {
                remove(cards.nthCard(i));
            }
        }

        /**
        * <p> Elimina la nth Carta del conjunto (partiendo de 1).
        * </p>
        * @param n nth carta a eliminar del conjunto.
        */
        public void remove(int n)
        {
            cardsSet.RemoveAt(n - 1);
        }

        /**
        * <p> Elimina la carta dada si es que se encuentra en el conjunto.
        * </p>
        * @param card carta a eliminar del conjunto.
        */
        public void remove(Card card)
        {
            for (int i = 1; i <= numCards(); i++)
            {
                if (card.Equals(nthCard(i)))
                {
                    remove(i);
                }
            }
        }

        /**
        * <p> Elimina todas las cartas del conjunto.
        * </p>
        */
        public void clear()
        {
            cardsSet.Clear();
        }

        /**
        * <p> Verifica si una carta pertenece al conjunto.
        * </p>
        * @param card carta a verifcar.
        * @return true si el conjunto contiene la carta dada, false sino la
        *           contiene.
        */
        public bool contains(Card card)
        {
            for (int i = 1; i <= numCards(); i++)
            {
                if (card.Equals(nthCard(i)))
                {
                    return true;
                }
            }
            return false;
        }

        /**
        * <p> Cuenta las apariciones de un elemento en el set de cartas.
        * </p>
        * @param element elemento a contar.
        * @return numero de apariciones del elmento dado.
        */
        public int elementOccurrences(string element)
        {
            int count = 0;
            for (int i = 1; i <= numCards(); i++)
            {
                if (nthCard(i).contains(element))
                {
                    count++;
                }
            }
            return count;
        }

        /**
        * <p> Revuelve las cartas.
        * </p>
        */
        public void mix()
        {
            Random rand = new Random();
            int randNumChanges = (int)(rand.NextInt64(numCards() * 2) + numCards());
            for (int i = 1; i <= randNumChanges; i++)
            {
                int n = (int)rand.NextInt64(1, numCards());
                Card nCard = nthCard(n);
                remove(n);
                int newN = (int)rand.NextInt64(numCards());
                cardsSet.Insert(newN, nCard);
            }
        }

        /**
        * <p> Compara this con otro Objeto, para esto compara si son de la misma
        *      clase (CardsSet) y luego si los dos conjuntos poseen las mismas
        *      cartas.
        * </p>
        * @param object objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public override bool Equals(object? o)
        {
            if (o != null && o.GetType().Equals(GetType()))
            {
                CardsSet cS = (CardsSet)o;
                if (cS.numCards() == numCards())
                {
                    for (int i = 1; i <= cS.numCards(); i++)
                    {
                        if (!contains(cS.nthCard(i)))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            return false;
        }

        /**
        * <p> Pasa la representacion del Conjunto de cartas a String.
        * </p>
        * @return String en representacion del conjunto de cartas.
        */

        public override string ToString()
        {
            string str = "";
            for (int i = 1; i <= numCards(); i++)
            {
                string n = i + ": ";
                str += "Card n" + n + nthCard(i).ToString() + "\n";
            }
            return str;
        }
    }
}
