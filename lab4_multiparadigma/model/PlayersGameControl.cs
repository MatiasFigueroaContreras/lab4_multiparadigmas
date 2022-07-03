using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    /**
     * Representa lo referente al control de jugadores de un juego.
     * @author Matias Figueroa Contreras
     */
    internal class PlayersGameControl
    {
        /**
        * Maxima cantidad de jugadores a registrar.
        */
        public int maxPlayers;

        /**
        * Turno del jugador actual.
        */
        public int playerTurn;

        /**
        * Arreglo con los jugadores, inicialmente vacio.
        */
        public List<Player> players = new();

        /**
        * <p> Constructor, inicializa el turno de jugador en 1 y asigna el maximo
        *       de jugadores dado.
        * </p>
        * @param maxPlayers maxima cantidad de jugadores a registrar.
        * @return el objeto PlayersGameControl creado.
        */
        public PlayersGameControl(int maxPlayers)
        {
            if (maxPlayers >= 1)
            {
                this.maxPlayers = maxPlayers;
                this.playerTurn = 1;
            }
        }

        /**
        * <p> Getter.
        * </p>
        * @return cantidad de jugadores registrados.
        */
        public int getTotalPlayers()
        {
            return this.players.Count;
        }

        /**
        * <p> Getter.
        * </p>
        * @return nombre del jugador que tiene el turno actual.
        */
        public String getPlayerTurn()
        {
            return nthPlayer(this.playerTurn).getName();
        }

        /**
        * <p> Getter.
        * </p>
        * @param name nombre del jugador que se desea saber el puntaje.
        * @return puntaje del jugador dado.
        */
        public int getPlayerScore(String name)
        {
            Player p = getPlayer(name);
            if (p != null)
            {
                return p.getScore();
            }
            return 0;
        }

        /**
        * <p> Suma un puntaje dado al jugador que tiene el turno actual.
        * </p>
        * @param score puntaje a sumar.
        */
        public void addScoreCurrentPlayerTurn(int score)
        {
            nthPlayer(this.playerTurn).addScore(score);
        }

        /**
        * <p> Suma el puntaje dado al jugador con el nombre dado.
        * </p>
        * @param score puntaje a sumar.
        * @param name nombre del jugador a sumar el puntaje.
        */
        public void addScorePlayer(int score, String name)
        {
            getPlayer(name).addScore(score);
        }

        /**
        * <p> Agrega cartas dadas al jugador que tiene el turno actual.
        * </p>
        * @param cards cartas a agregar.
        */
        public void addCardsCurrentPlayerTurn(CardsSet cards)
        {
            nthPlayer(this.playerTurn).addCards(cards);
        }

        /**
        * <p> Agrega cartas dado al jugador con el nombre dado.
        * </p>
        * @param cards cartas a agregar.
        * @param name nombre del jugador a agregar las cartas.
        */
        public void addCardsPlayer(CardsSet cards, String name)
        {
            getPlayer(name).addCards(cards);
        }

        /**
        * <p> A�ade un jugador al arreglo (this.players), respetando que este no 
        *       sea parte de este, que no se supere el maximo de jugadores a 
        *       registrar m�s espacios reservados, y que no contenga el nombre 
        *       reservado CPU.
        * </p>
        * @param name nombre del jugador a registrar/ agregar a la lista de jugadores.
        * @param reservedSlots cantidad de espacios reservados para registrar jugadores.
        */
        public void addPlayer(String name, int reservedSlots)
        {
            Player p = new Player(name);
            if ((reservedSlots >= 0) && (getTotalPlayers() < (this.maxPlayers - reservedSlots)) && !contains(p) && (p.getName() != null) && (!name.Contains("CPU")))
            {
                this.players.Add(p);
            }
        }

        /**
        * <p> A�ade un jugador al arreglo (this.players), respetando que este no 
        *       sea parte de este, que no se supere el maximo de jugadores a 
        *       registrar.
        * </p>
        * @param name nombre del jugador a registrar/ agregar a la lista de jugadores.
        */
        public void addPlayer(String name)
        {
            Player p = new Player(name);
            if ((getTotalPlayers() < this.maxPlayers) && !contains(p) && (p.getName() != null))
            {
                this.players.Add(p);
            }
        }

        /**
        * <p> Busca el nth Jugador de la lista de jugadores(this.players), 
        *       partiendo desde 1.
        * </p>
        * @param n indice (nth) a buscar en la lista.
        * @return el nth jugador buscado.
        */
        public Player nthPlayer(int n)
        {
            return this.players[n - 1];
        }

        /**
        * <p> Verifica si un jugador pertenece a la lista.
        * </p>
        * @param player jugador a verifcar.
        * @return true si el jugador se encuentra en la lista, false sino esta.
        */
        public bool contains(Player player)
        {
            for (int i = 1; i <= getTotalPlayers(); i++)
            {
                if (player.Equals(nthPlayer(i)))
                {
                    return true;
                }
            }
            return false;
        }

        /**
        * <p> Getter, busca al jugador con el nombre dado.
        * </p>
        * @param name nombre del jugador a buscar.
        * @return el jugador que tenga el nombre dado.
        */
        public Player? getPlayer(String name)
        {
            Player p = new Player(name);
            Player pi;
            for (int i = 1; i <= getTotalPlayers(); i++)
            {
                pi = nthPlayer(i);
                if (p.Equals(pi))
                {
                    return pi;
                }
            }
            return null;
        }

        /**
        * <p> Pasa al siguiente turno.
        * </p>
        */
        public void nextTurn()
        {
            if (this.playerTurn >= players.Count)
            {
                this.playerTurn = 1;
            }
            else
            {
                this.playerTurn++;
            }
        }

        /**
        * <p> Obtiene el puntaje m�s alto entre los jugadores.
        * </p>
        * @return el puntaje mas alto entre los jugadores.
        */
        private int highestScore()
        {
            int h = nthPlayer(1).getScore();
            for (int i = 2; i <= getTotalPlayers(); i++)
            {
                int nScore = nthPlayer(i).getScore();
                if (h < nScore)
                {
                    h = nScore;
                }
            }

            return h;
        }

        /**
        * <p> Getter, obtiene el/los jugadores que tienen el mayor puntaje.
        * </p>
        * @return arreglo con los jugadores que ganaron/ estan ganando.
        */
        public List<String> getWinners()
        {
            int h = highestScore();
            List<String> winners = new();
            for (int i = 1; i <= getTotalPlayers(); i++)
            {
                Player nPlayer = nthPlayer(i);
                if (h == nPlayer.getScore())
                {
                    winners.Add(nPlayer.getName());
                }
            }
            return winners;
        }

        /**
        * <p> Getter, obtiene el/los jugadores que estan por debajo del mayor
        *       puntaje.
        * </p>
        * @return arreglo con los jugadores que perdieron/ estan perdiendo.
        */
        public List<String> getLosers()
        {
            int h = highestScore();
            List<String> losers = new();
            for (int i = 1; i <= getTotalPlayers(); i++)
            {
                Player nPlayer = nthPlayer(i);
                if (h > nPlayer.getScore())
                {
                    losers.Add(nPlayer.getName());
                }
            }
            return losers;
        }

        /**
        * <p> Compara this con otro Objeto, para esto compara si son de la misma
        *      clase (PlayersGameControl) y luego si los dos Objetos tienen los
        *      mismos valores en sus atributos.
        * </p>
        * @param object objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public override bool Equals(Object? o)
        {
            if (o != null && o.GetType().Equals(this.GetType()))
            {
                PlayersGameControl pGC = (PlayersGameControl)o;
                return (this.maxPlayers == pGC.maxPlayers) && (this.playerTurn == pGC.playerTurn) && this.players.Equals(pGC);
            }
            return false;
        }

        /**
        * <p> Pasa la representacion de los jugadores a String
        * </p>
        * @return String en representacion de los jugadores registrados.
        */
        public override String ToString()
        {
            String str = "";
            for (int i = 1; i <= getTotalPlayers(); i++)
            {
                String n = i + ": ";
                str += "Player n" + n + nthPlayer(i).ToString() + "\n";
            }
            return str;
        }
    }
}
