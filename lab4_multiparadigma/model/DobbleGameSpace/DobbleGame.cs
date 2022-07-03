using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model.DobbleGameSpace
{
    /**
     * Representa un juego dobble, el cual posee un control de jugadores, un area
     *  de juego en donde estara presente el mazo Dobble, un modo de juego el
     *  cual estar encargado de controlar jugadas y otros aspectos, tambien un
     *  estado que permite saber en que se encuentra el juego y controlar este.
     * @author Matias Figueroa Contreras
     */
    public class DobbleGame : IDobbleGame
    {
        /**
        * Nombre que se le dara al juego.
        */
        private string name;

        /**
        * Area de juego que contendra el mazo Dobble y las cartas en juego.
        */
        private GameArea gameArea;

        /**
        * Control de jugadores, gestiona todo lo relativo a estos.
        */
        private PlayersGameControl playersGameControl;

        /**
        * Modo de juego, gestiona las jugadas y la asignacion de puntos entre otros.
        */
        private Mode mode;

        /**
        * Estado en el que se encuentra el juego, inicialmente esperando el inicio
        *   de este.
        */
        private string status = "Esperando inicio del juego";

        /**
        * <p> Constructor, asigna el nombre del juego, el maximo de jugadores
        *       al gestor de jugadores, crea el modo de juego dado, e inicializa
        *       el area de juego con los datos dados para crear el mazo Dobble.
        * </p>
        * @param gameName nombre con el que se identificara el juego.
        * @param maxP maximo de jugadores que se podran registrar dado al gestor
        *               de jugadores.
        * @param mode modo de juego que se creara (si se encuentra disponible).
        * @param elements elementos con los que se completara el mazo de cartas 
        *                   Dobble, del area de juego.
        * @param numE numero de elementos por carta, que tendra el mazo de cartas 
        *                   Dobble, del area de juego.
        * @param maxC maximo numero de cartas que tendra el mazo de cartas Dobble,
        *               del area de juego.
        * @return el objeto DobbleGame creado.
        */
        public DobbleGame(string gameName, int maxP, string mode, List<string> elements, int numE, int maxC)
        {
            Mode? m = stringToMode(mode);
            if (m == null)
            {
                throw new DobbleGameException(400, "Modo de juego no disponible.");
            }
            if (maxP <= m.getMaxPlayers() && maxP >= m.getMinPlayers())
            {
                playersGameControl = new PlayersGameControl(maxP + m.getExtraPlayers());
            }
            else
            {
                throw new DobbleGameException(400, "El maximo de jugadores a registrar no concuerda con el modo de juego escogido.");
            }
            name = gameName;
            gameArea = new GameArea(elements, numE, maxC);
            this.mode = m;
        }

        /**
        * <p> dado un String se�alando el modo, se crea este objeto Mode, si es que
        *       esta disponible para se inicializado.
        * </p>
        * @param mode modo de juego a crear.
        * @return el objeto Mode creado si se encuentra dentro de los disponibles,
        *           en otro caso null.
        */
        private Mode? stringToMode(string mode)
        {
            switch (mode)
            {
                case "Stack Player vs CPU":
                    {
                        return new StackPlayerVsCpuMode();
                    }
                default:
                    {
                        return null;
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
            return GameArea.totalCardsNumElements(numE);
        }

        /**
        * <p> Da inicio al juego, si es que este todavia no lo estaba y si se cumple
        *       con los jugadores minimos registrados que establece el modo de juego.
        * </p>
        * @return true si se pudo iniciar el juego, false si el juego ya estaba
        *           iniciado o no cumplia los requisitos.
        */
        public void start()
        {
            if (status.Equals("Esperando inicio del juego") && playersGameControl.getTotalPlayers() >= mode.getMinPlayers())
            {
                status = mode.start(this);
            }
            else
            {
                throw new DobbleGameException(500, "Juego ya iniciado, o terminado.");
            }
        }

        /**
        * <p> Permite realizar una jugada segun el modo de juego activo, esto
        *       controlando que el juego no este terminado ni aun sin empezar, y
        *       que la jugada pertenezca al modo de juego.
        * </p>
        * @param option opcion escogida para realizar la jugada.
        * @return true si la jugada pudo ser efectuada, false sino pudo ser 
        *           efectuada.
        */
        public void play(string option)
        {
            if (status.Equals("Juego Terminado"))
            {
                throw new DobbleGameException(501, "El juego ya ha finalizado.");
            }
            if (status.Equals("Esperando inicio del juego"))
            {
                throw new DobbleGameException(502, "Juego aun no comenzado.");
            }
            string newStatus = mode.play(this, option);
            status = newStatus;
        }

        /**
        * <p> Permite realizar una jugada segun el modo de juego activo, esto
        *       controlando que el juego no este terminado ni aun sin empezar, y
        *       que la jugada pertenezca al modo de juego, ademas incluye 
        *       informacion extra que pueda necesitar el modo de juego para realizar
        *       la jugada.
        * </p>
        * @param option opcion escogida para realizar la jugada.
        * @param data lista con la informacion que se necesite para realizar la
        *               jugada.
        * @return true si la jugada pudo ser efectuada, false sino pudo ser 
        *           efectuada.
        */
        public void play(string option, string[] data)
        {
            if (status.Equals("Juego Terminado"))
            {
                throw new DobbleGameException(501, "El juego ya ha finalizado.");
            }
            if (status.Equals("Esperando inicio del juego"))
            {
                throw new DobbleGameException(502, "Juego aun no comenzado.");
            }
            string newStatus = mode.play(this, option, data);
            status = newStatus;
        }

        /**
        * <p> Termina el juego cambiando el estado de este.
        * </p>
        */
        public void finish()
        {
            status = "Juego Terminado";
        }

        /**
        * <p> Pasa al siguiente turno, a traves del gestor de jugadores.
        * </p>
        */
        internal void nextTurn()
        {
            playersGameControl.nextTurn();
        }

        internal void addCardsInPlayCurrentPlayerTurn()
        {
            playersGameControl.addCardsCurrentPlayerTurn(gameArea.getCardsInPlay());
        }

        /**
        * <p> Agrega las cartas en juego al jugador con el nombre dado, a traves del 
        *       gestor de jugadores.
        * </p>
        * @param cards cartas a agregar.
        * @param name nombre del jugador a agregar las cartas.
        */
        internal void addCardsInPlayPlayer(string name)
        {
            playersGameControl.addCardsPlayer(gameArea.getCardsInPlay(), name);
        }

        /**
        * <p> Suma un puntaje dado al jugador que tiene el turno actual, a traves del
        *       gestor de jugadores.
        * </p>
        * @param score puntaje a sumar.
        */
        internal void addScoreCurrentPlayerTurn(int score)
        {
            playersGameControl.addScoreCurrentPlayerTurn(score);
        }

        /**
        * <p> Suma el puntaje dado al jugador con el nombre dado a traves del
        *       gestor de jugadores.
        * </p>
        * @param score puntaje a sumar.
        * @param name nombre del jugador a sumar el puntaje.
        */
        internal void addScorePlayer(int score, string name)
        {
            playersGameControl.addScorePlayer(score, name);
        }

        /**
        * <p> Agrega cartas desde un indice inicial hasta uno final del mazo 
        *       Dobble a las cartas en juego,  a travez del area de juego.
        * </p>
        * @param start punto de partida del indice de cartas.
        * @param end punto de llegada del indice de cartas.
        */
        internal void addDobbleCardsInPlay(int start, int end)
        {
            gameArea.addDobbleCardsInPlay(start, end);
        }

        /**
        * <p> Devuelve las cartas en juego al mazo Dobble, a travez
        *       del area de juego.
        * </p>
        */
        internal void backCardsInPlay()
        {
            gameArea.backCardsInPlay();
        }

        /**
        * <p> Retira todas las cartas que estan en juego, a traves del area 
        *       de juego.
        * </p>
        */
        internal void clearCardsInPlay()
        {
            gameArea.clearCardsInPlay();
        }

        /**
        * <p> Cuenta las apariciones de un elemento en las cartas en juego, 
        *       presentes en el area de juego.
        * </p>
        * @param element elemento a contar.
        * @return numero de apariciones del elmento dado.
        */
        internal int elementOccurrencesCardsInPlay(string element)
        {
            return gameArea.elementOccurrencesCardsInPlay(element);
        }

        /**
        * <p> Busca el nth Elemento en su forma de String del mazo de cartas Dobble,
        *       que gestiona el area de juego, partiendo desde 1.
        * </p>
        * @param n indice (nth) a buscar en el mazo.
        * @return el nth elemento buscado en su representacion de String.
        */
        internal string nthElement(int n)
        {
            return gameArea.nthElement(n);
        }

        /**
        * <p> Cuenta la cantidad de elementos con los que trabaja el mazo Dobble,
        *       a traves del area de juego.
        * </p>
        * @return la cantidad de elementos con los que trabaja el mazo Dobble.
        */
        internal int numElements()
        {
            return gameArea.numElements();
        }

        /**
        * <p> Cuenta la cantidad de cartas que hay en juego, a traves del area 
        *       de juego.
        * </p>
        * @return cantidad de cartas que hay en juego.
        */
        internal int numCardsInPlay()
        {
            return gameArea.numCardsInPlay();
        }

        /**
        * <p> Cuenta la cantidad de cartas que tiene el mazo Dobble, a traves del area 
        *       de juego.
        * </p>
        * @return cantidad de cartas que tiene el mazo Dobble.
        */
        internal int numDobbleCards()
        {
            return gameArea.numDobbleCards();
        }

        /**
        * <p> A�ade un jugador al controlador de estos, teniendo en cuenta los
        *       jugadores que necesitara registrar el modo de juego.
        * </p>
        * @param name nombre del jugador a registrar.
        */
        public void register(string name)
        {
            playersGameControl.addPlayer(name, mode.getExtraPlayers());
        }

        /**
        * <p> A�ade un jugador extra al controlador de estos, este metodo esta
        *       pensado para que el modo de juego pueda registrar jugadores
        *       especiales.
        * </p>
        * @param name nombre del jugador a registrar.
        */
        internal void registerExtra(string name)
        {
            playersGameControl.addPlayer(name);
        }

        /**
        * <p> Consulta al controlador de jugadores de quien es el turno actual.
        * </p>
        * @return nombre del jugador al cual le toca jugar.
        */
        public string whoseTurnIsIt()
        {
            return playersGameControl.getPlayerTurn();
        }

        /**
        * <p> Consulta al controlador de jugadores el puntaje de un jugador dado.
        * </p>
        * @param name nombre del jugador que se desea saber el puntaje.
        * @return puntaje del jugador consultado.
        */
        public int getScore(string name)
        {
            return playersGameControl.getPlayerScore(name);
        }

        /**
        * <p> Getter, consulta al modo de juego su nombre.
        * </p>
        * @return el nombre del modo de juego.
        */
        public string getNameOfMode()
        {
            return mode.getModeName();
        }

        /**
        * <p> Getter.
        * </p>
        * @return la version del modo de juego.
        */
        public string getVersionMode()
        {
            return mode.getVersionModeName();
        }

        /**
        * <p> Getter, consulta al modo de juego si dado una opcion de juego, y el
        *       estado del juego, se necesita informacion extra y cual es el nombre
        *       de esta.
        * </p>
        * @param option opcion de juego
        * @return nombre de la informacion extra necesitada, o null si no se 
        *           necesita informacion extra.
        */
        public string? getExtraDataNeeded(string option)
        {
            return mode.extraDataNeeded(status, option);
        }

        /**
        * <p> Getter.
        * </p>
        * @return cantidad de informacion extra que se necesitara.
        */
        public int getNumExtraDataNeded()
        {
            return mode.numExtraDataNeeded(this);
        }

        /**
        * <p> Getter.
        * </p>
        * @return copia del nombre del juego.
        */
        public string getGameName()
        {
            string gameNameCopy = new string(name);
            return gameNameCopy;
        }

        /**
        * <p> Getter.
        * </p>
        * @return copia del estado del juego.
        */
        public string getStatus()
        {
            string statusCopy = new string(status);
            return statusCopy;
        }

        /**
        * <p> Setter, cambia el estado de juego (this.status) por una copia del dado.
        * </p>
        * @param newStatus nuevo estado a setear.
        */
        internal void setStatus(string newStatus)
        {
            status = new string(newStatus);
        }

        /**
        * <p> Consulta si el juego esta terminado.
        * </p>
        * @return true si el juego esta terminado, false sino lo esta.
        */
        public bool isFinished()
        {
            return status.Equals("Juego Terminado");
        }

        /**
        * <p> Pide al Area de juego las cartas en juego representada en string.
        * </p>
        * @return cartas en juego representadas en string.
        */
        public string cardsInPlayString()
        {
            return gameArea.cardsInPlayToString();
        }

        /**
        * <p> Getter.
        * </p>
        * @return opciones de juego segun el modo activo, si es que el juego
        *           no ha terminado, caso contrario ninguna opcion de juego.
        */
        public string[] getPlaysOptions()
        {
            if (status.Equals("Juego Terminado"))
            {
                throw new DobbleGameException(501, "El juego ya ha finalizado.");
            }

            return mode.playsOptions(this);
        }

        /**
        * <p> Getter.
        * </p>
        * @return ganadores del juego representado en string.
        */
        public string? getWinners()
        {
            if (status.Equals("Juego Terminado"))
            {
                return playersGameControl.getWinners().ToString();
            }

            throw new DobbleGameException(503, "El juego aun no ha terminado.");
        }

        /**
        * <p> Getter.
        * </p>
        * @return perdedores del juego representado en string.
        */
        public string? getLosers()
        {
            if (status.Equals("Juego Terminado"))
            {
                return playersGameControl.getLosers().ToString();
            }

            throw new DobbleGameException(503, "El juego aun no ha terminado.");
        }

        /**
        * <p> Getter.
        * </p>
        * @return jugadores registrados en su representacion de String.
        */
        public string registeredPlayers()
        {
            return playersGameControl.ToString();
        }

        /**
        * <p> Compara this con otro Objeto, para esto compara si son de la misma
        *      clase (DobbleGame) y luego si los dos juegos tienen el mismo nombre
        *      o sus datos son los mismos.
        * </p>
        * @param object objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public override bool Equals(object? o)
        {
            if (o != null && o.GetType().Equals(GetType()))
            {
                DobbleGame dG = (DobbleGame)o;
                return name.Equals(dG.getGameName()) || playersGameControl.Equals(dG.playersGameControl) && status == dG.status && mode.Equals(dG.mode) && gameArea.Equals(dG.gameArea);
            }
            return false;
        }

        /**
        * <p> Pasa la representacion del juego Dobble a String.
        * </p>
        * @return String en representacion del juego Dobble.
        */
        public override string ToString()
        {
            string gameName = "Nombre del juego: " + name;
            string modeName = "\nModo de juego: " + getNameOfMode() + ", en su version: " + getVersionMode();
            string st = "Estado del Juego: " + getStatus();
            string cards = "Cartas en juego:\n" + cardsInPlayString();
            string players = "Jugadores registrados:\n" + registeredPlayers();
            string jump = "\n--------------\n";
            string strFinal = gameName + modeName + jump + st + jump + cards + jump + players;
            if (status == "Juego Terminado")
            {
                string winners = "Ganadores:\n" + playersGameControl.getWinners();
                string losers = "Perdedores:\n" + playersGameControl.getLosers();
                string results = "Resultados Finales:\n" + winners + "\n" + losers;
                strFinal += jump + results;
            }
            return strFinal;
        }
    }
}
