using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model.DobbleGameSpace
{
    /**
     * Representa a un jugador con su nombre, puntaje y cartas asociadas.
     * @author Mat�as Figueroa Contreras 
     */
    internal class Player
    {
        /**
        * Nombre del jugador.
        */
        private string name;

        /**
        * Puntaje del jugador.
        */
        private int score;

        /**
        * Cartas del jugador, inicialmente sin cartas.
        */
        private CardsSet cards = new CardsSet();

        /**
        * <p> Constructor, crea el jugador asignando su nombre, y puntaje en 0.
        * </p>
        * @param name nombre del jugador a crear.
        * @return el objeto Player creado.
        */
        public Player(string name)
        {
            if (name.Replace(" ", "").Length > 0)
            {
                this.name = name;
                score = 0;
            }
            else
            {
                throw new DobbleGameException(701, "Nombre de jugador no valido");
            }
        }

        /**
        * <p> Suma puntaje al que ya tenia el jugdor (this).
        * </p>
        * @param score puntaje a sumar.
        */
        public void addScore(int score)
        {
            this.score += score;
        }

        /**
        * <p> Agrega cartas a las que ya tenia el jugdor (this).
        * </p>
        * @param cards cartas a agregar.
        */
        public void addCards(CardsSet cards)
        {
            this.cards.union(cards);
        }

        /**
        * <p> Setter, que cambia el nombre del jugador (this).
        * </p>
        * @param name nombre a cambiar del jugador.
        */
        public void setName(string name)
        {
            if (name.Replace(" ", "").Length > 0)
            {
                this.name = name;
            }
        }

        /**
        * <p> Setter, que cambia el puntaje del jugador (this).
        * </p>
        * @param score puntaje a cambiar del jugador.
        */
        public void setScore(int score)
        {
            this.score = score;
        }

        /**
        * <p> Setter, que cambia las cartas del jugador (this).
        * </p>
        * @param cards cartas a cambiar del jugador.
        */
        public void setCards(CardsSet cards)
        {
            this.cards = cards;
        }

        /**
        * <p> Getter, del nombre.
        * </p>
        * @return nombre del jugador.
        */
        public string getName()
        {
            return name;
        }

        /**
        * <p> Getter, del puntaje.
        * </p>
        * @return puntaje del jugador.
        */
        public int getScore()
        {
            return score;
        }

        /**
        * <p> Getter, de las cartas.
        * </p>
        * @return cartas del jugador.
        */
        public CardsSet getCards()
        {
            return cards;
        }

        /**
        * <p> Pasa la representacion del Jugador a String, con su nombre y puntaje
        *       asociado.
        * </p>
        * @return String en representacion del jugador.
        */
        public override string ToString()
        {
            return "Nombre: " + name + ", Puntaje: " + score;
        }

        /**
        * <p> Compara this con otro Objeto, para esto compara si son de la misma
        *      clase (Player) y luego si los nombres tienen el mismo valor.
        * </p>
        * @param object objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public override bool Equals(object? o)
        {
            if (o != null && o.GetType().Equals(GetType()))
            {
                Player p = (Player)o;
                return getName().Equals(p.name);
            }
            return false;
        }
    }
}
