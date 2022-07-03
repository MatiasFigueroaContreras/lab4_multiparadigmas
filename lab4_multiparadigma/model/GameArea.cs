using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    /**
     * Representa el area de juego, en donde se representa el mazo de cartas Dobble 
     *  inicial (this.dobbleSet) y las cartas que seran volteadas en el juego (this.cardsInPlay)
     * @author Matias Figueroa Contreras
     */
    internal class GameArea
    {
        /**
        * El mazo de cartas Dobble de donde se sacaran las cartas en el juego
        */
        private Dobble dobbleSet;

        /**
        * Las cartas que estaran en juego, inicialmente vacia.
        */
        private CardsSet cardsInPlay = new CardsSet();


        /**
        * <p> Constructor, asigna el mazo de cartas Dobble pasado al del juego.
        * </p>
        * @param dobbleSet mazo de cartas Dobble.
        * @return el objeto GameArea creado.
        */
        public GameArea(Dobble dobbleSet)
        {
            this.dobbleSet = dobbleSet;
        }


        /**
        * <p> Constructor, crea un mazo de cartas Dobble segun los datos entregados.
        * </p>
        * @param elements listado de elementos con el que se creara el mazo Dobble.
        * @param numE numero de elementos por cartas con el que se creara el mazo.
        * @param maxC maximo de cartas que contendra el mazo Dobble a crear.
        * @return el objeto GameArea creado.
        */
        public GameArea(List<String> elements, int numE, int maxC)
        {
            this.dobbleSet = new Dobble(elements, numE, maxC);
        }

        /**
        * <p> Agrega la nth carta del mazo Dobble(this.dobbleSet) a las cartas
        *       en juego (this.cardsInPlay).
        * </p>
        * @param n nth carta a pasar.
        */
        public void addDobbleCardInPlay(int n)
        {
            this.cardsInPlay.add(this.dobbleSet.nthCard(n));
            this.dobbleSet.removeCard(n);
        }


        /**
        * <p> Agrega cartas desde un indice inicial hasta uno final del mazo 
        *       Dobble(this.dobbleSet) a las cartas en juego (this.cardsInPlay).
        * </p>
        * @param start punto de partida del indice de cartas.
        * @param end punto de llegada del indice de cartas.
        */
        public void addDobbleCardsInPlay(int start, int end)
        {
            if (start <= end)
            {
                for (int i = start; i <= end; i++)
                {
                    addDobbleCardInPlay(start);
                }
            }
        }

        /**
        * <p> Devuelve las cartas en juego al mazo Dobble.
        * </p>
        */
        public void backCardsInPlay()
        {
            for (int i = 1; i <= this.cardsInPlay.numCards(); i++)
            {
                this.dobbleSet.addCard(this.cardsInPlay.nthCard(i));
            }
            this.cardsInPlay.clear();
        }

        /**
        * <p> Cuenta las apariciones de un elemento en las cartas en juego.
        * </p>
        * @param element elemento a contar.
        * @return numero de apariciones del elmento dado.
        */
        public int elementOccurrencesCardsInPlay(String element)
        {
            return this.cardsInPlay.elementOccurrences(element);
        }

        /**
        * <p> Getter.
        * </p>
        * @return el mazo de cartas Dobble.
        */
        public Dobble getDobbleSet()
        {
            return this.dobbleSet;
        }

        /**
        * <p> Getter.
        * </p>
        * @return el conjunto de cartas en juego.
        */
        public CardsSet getCardsInPlay()
        {
            return this.cardsInPlay;
        }

        /**
        * <p> Setter, cambia el mazo de cartas Dobble por uno dado.
        * </p>
        * @param dobbleSet mazo de cartas a agregar.
        */
        public void setDobble(Dobble dobbleSet)
        {
            this.dobbleSet = dobbleSet;
        }

        /**
        * <p> Setter, cambia las cartas en juego por las dadas.
        * </p>
        * @param cards conjunto de cartas a agregar.
        */
        public void setCardsInPlay(CardsSet cards)
        {
            this.cardsInPlay = cards;
        }

        /**
        * <p> Pasa la representacion de las cartas en juego a String.
        * </p>
        * @return String en representacion de las cartas en juego.
        */
        public String cardsInPlayToString()
        {
            return this.cardsInPlay.ToString();
        }

        /**
        * <p> Calcula el maximo de cartas que se pueden crear segun el numero de
        *       elementos que estas contienen, es decir el mazo completo Dobble.
        * </p>
        * @param numE numero de elementos por carta.
        */
        public static int totalCardsNumElements(int numE)
        {
            return Dobble.totalCardsNumElements(numE);
        }

        /**
        * <p> Busca el nth Elemento en su forma de String del mazo de cartas Dobble, 
        *       partiendo desde 1.
        * </p>
        * @param n indice (nth) a buscar en el mazo.
        * @return el nth elemento buscado en su representacion de String.
        */
        public String nthElement(int n)
        {
            return this.dobbleSet.nthElement(n);
        }

        /**
        * <p> Cuenta la cantidad de elementos con los que trabaja el mazo Dobble.
        * </p>
        * @return la cantidad de elementos con los que trabaja el mazo Dobble.
        */
        public int numElements()
        {
            return this.dobbleSet.numElements();
        }

        /**
        * <p> Cuenta la cantidad de cartas que hay en juego.
        * </p>
        * @return cantidad de cartas que hay en juego.
        */
        public int numCardsInPlay()
        {
            return this.cardsInPlay.numCards();
        }

        /**
        * <p> Cuenta la cantidad de cartas que tiene el mazo Dobble.
        * </p>
        * @return cantidad de cartas que tiene el mazo Dobble.
        */
        public int numDobbleCards()
        {
            return this.dobbleSet.numCards();
        }

        /**
        * <p> Elimina todas las cartas que estan en juego.
        * </p>
        */
        public void clearCardsInPlay()
        {
            this.cardsInPlay.clear();
        }

        /**
        * <p> Compara this con otro Objeto, para esto compara si son de la misma
        *      clase (GameArea) y luego si los dos objetos poseen el mismo mazo de
        *      cartas Dobble y las mismas cartas en juego.
        * </p>
        * @param object objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public override bool Equals(Object? o)
        {
            if (o != null && o.GetType().Equals(this.GetType()))
            {
                GameArea gA = (GameArea)o;
                return this.cardsInPlay.Equals(gA.cardsInPlay) && this.dobbleSet.Equals(gA.cardsInPlay);
            }
            return false;
        }
    }
}
