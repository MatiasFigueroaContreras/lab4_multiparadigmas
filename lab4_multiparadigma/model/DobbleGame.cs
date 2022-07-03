using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    /**
     * Representa un juego dobble, el cual posee un control de jugadores, un area
     *  de juego en donde estara presente el mazo Dobble, un modo de juego el
     *  cual estar encargado de controlar jugadas y otros aspectos, tambien un
     *  estado que permite saber en que se encuentra el juego y controlar este.
     * @author Matias Figueroa Contreras
     */
    internal class DobbleGame: IDobbleGame
    {
        /**
        * Nombre que se le dara al juego.
        */
        private String name;

        /**
        * Area de juego que contendra el mazo Dobble y las cartas en juego.
        */
        protected GameArea gameArea;

        /**
        * Control de jugadores, gestiona todo lo relativo a estos.
        */
        protected PlayersGameControl playersGameControl;

        /**
        * Modo de juego, gestiona las jugadas y la asignacion de puntos entre otros.
        */
        private Mode mode;

        /**
        * Estado en el que se encuentra el juego, inicialmente esperando el inicio
        *   de este.
        */
        private String status = "Esperando inicio del juego";

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
        public DobbleGame(String gameName, int maxP, String mode, List<String> elements, int numE, int maxC)
        {
            Mode m = stringToMode(mode);
            if (m == null)
            {
                return;
            }
            if (maxP <= m.getMaxPlayers() && maxP >= m.getMinPlayers())
            {
                this.playersGameControl = new PlayersGameControl(maxP + m.getExtraPlayers());
            }
            else
            {
                this.playersGameControl = new PlayersGameControl(m.getMaxPlayers() + m.getExtraPlayers());
            }
            this.name = gameName;
            this.gameArea = new GameArea(elements, numE, maxC);
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
        private Mode stringToMode(String mode)
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
        public bool start()
        {
            if (this.status.Equals("Esperando inicio del juego") && this.playersGameControl.getTotalPlayers() >= this.mode.getMinPlayers())
            {
                this.status = mode.start(this);
                return true;
            }
            return false;
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
        public bool play(String option)
        {
            if (!this.status.Equals("Juego Terminado") && !this.status.Equals("Esperando inicio del juego"))
            {
                String newStatus = mode.play(this, option);
                if (newStatus == null)
                {
                    return false;
                }
                else
                {
                    this.status = newStatus;
                    return true;
                }
            }
            return false;
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
        public bool play(String option, String[] data)
        {
            if (!this.status.Equals("Juego Terminado") && !this.status.Equals("Esperando inicio del juego"))
            {
                String newStatus = mode.play(this, option, data);
                if (newStatus == null)
                {
                    return false;
                }
                else
                {
                    this.status = newStatus;
                    return true;
                }
            }
            return false;
        }

        /**
        * <p> Termina el juego cambiando el estado de este.
        * </p>
        */
        public void finish()
        {
            this.status = "Juego Terminado";
        }

        /**
        * <p> Pasa al siguiente turno, a traves del gestor de jugadores.
        * </p>
        */
        internal void nextTurn()
        {
            this.playersGameControl.nextTurn();
        }

        internal void addCardsInPlayCurrentPlayerTurn()
        {
            this.playersGameControl.addCardsCurrentPlayerTurn(this.gameArea.getCardsInPlay());
        }

        /**
        * <p> Agrega las cartas en juego al jugador con el nombre dado, a traves del 
        *       gestor de jugadores.
        * </p>
        * @param cards cartas a agregar.
        * @param name nombre del jugador a agregar las cartas.
        */
        internal void addCardsInPlayPlayer(String name)
        {
            this.playersGameControl.addCardsPlayer(this.gameArea.getCardsInPlay(), name);
        }

        /**
        * <p> Suma un puntaje dado al jugador que tiene el turno actual, a traves del
        *       gestor de jugadores.
        * </p>
        * @param score puntaje a sumar.
        */
        internal void addScoreCurrentPlayerTurn(int score)
        {
            this.playersGameControl.addScoreCurrentPlayerTurn(score);
        }

        /**
        * <p> Suma el puntaje dado al jugador con el nombre dado a traves del
        *       gestor de jugadores.
        * </p>
        * @param score puntaje a sumar.
        * @param name nombre del jugador a sumar el puntaje.
        */
        internal void addScorePlayer(int score, String name)
        {
            this.playersGameControl.addScorePlayer(score, name);
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
            this.gameArea.addDobbleCardsInPlay(start, end);
        }

        /**
        * <p> Devuelve las cartas en juego al mazo Dobble, a travez
        *       del area de juego.
        * </p>
        */
        internal void backCardsInPlay()
        {
            this.gameArea.backCardsInPlay();
        }

        /**
        * <p> Retira todas las cartas que estan en juego, a traves del area 
        *       de juego.
        * </p>
        */
        internal void clearCardsInPlay()
        {
            this.gameArea.clearCardsInPlay();
        }

        /**
        * <p> Cuenta las apariciones de un elemento en las cartas en juego, 
        *       presentes en el area de juego.
        * </p>
        * @param element elemento a contar.
        * @return numero de apariciones del elmento dado.
        */
        internal int elementOccurrencesCardsInPlay(String element)
        {
            return this.gameArea.elementOccurrencesCardsInPlay(element);
        }

        /**
        * <p> Busca el nth Elemento en su forma de String del mazo de cartas Dobble,
        *       que gestiona el area de juego, partiendo desde 1.
        * </p>
        * @param n indice (nth) a buscar en el mazo.
        * @return el nth elemento buscado en su representacion de String.
        */
        internal String nthElement(int n)
        {
            return this.gameArea.nthElement(n);
        }

        /**
        * <p> Cuenta la cantidad de elementos con los que trabaja el mazo Dobble,
        *       a traves del area de juego.
        * </p>
        * @return la cantidad de elementos con los que trabaja el mazo Dobble.
        */
        internal int numElements()
        {
            return this.gameArea.numElements();
        }

        /**
        * <p> Cuenta la cantidad de cartas que hay en juego, a traves del area 
        *       de juego.
        * </p>
        * @return cantidad de cartas que hay en juego.
        */
        internal int numCardsInPlay()
        {
            return this.gameArea.numCardsInPlay();
        }

        /**
        * <p> Cuenta la cantidad de cartas que tiene el mazo Dobble, a traves del area 
        *       de juego.
        * </p>
        * @return cantidad de cartas que tiene el mazo Dobble.
        */
        internal int numDobbleCards()
        {
            return this.gameArea.numDobbleCards();
        }

        /**
        * <p> A�ade un jugador al controlador de estos, teniendo en cuenta los
        *       jugadores que necesitara registrar el modo de juego.
        * </p>
        * @param name nombre del jugador a registrar.
        */
        public void register(String name)
        {
            playersGameControl.addPlayer(name, this.mode.getExtraPlayers());
        }

        /**
        * <p> A�ade un jugador extra al controlador de estos, este metodo esta
        *       pensado para que el modo de juego pueda registrar jugadores
        *       especiales.
        * </p>
        * @param name nombre del jugador a registrar.
        */
        internal void registerExtra(String name)
        {
            playersGameControl.addPlayer(name);
        }

        /**
        * <p> Consulta al controlador de jugadores de quien es el turno actual.
        * </p>
        * @return nombre del jugador al cual le toca jugar.
        */
        public String whoseTurnIsIt()
        {
            return playersGameControl.getPlayerTurn();
        }

        /**
        * <p> Consulta al controlador de jugadores el puntaje de un jugador dado.
        * </p>
        * @param name nombre del jugador que se desea saber el puntaje.
        * @return puntaje del jugador consultado.
        */
        public int getScore(String name)
        {
            return playersGameControl.getPlayerScore(name);
        }

        /**
        * <p> Getter, consulta al modo de juego su nombre.
        * </p>
        * @return el nombre del modo de juego.
        */
        public String getNameOfMode()
        {
            return this.mode.getModeName();
        }

        /**
        * <p> Getter.
        * </p>
        * @return la version del modo de juego.
        */
        public String getVersionMode()
        {
            return this.mode.getVersionModeName();
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
        public String getExtraDataNeeded(String option)
        {
            return this.mode.extraDataNeeded(this.status, option);
        }

        /**
        * <p> Getter.
        * </p>
        * @return cantidad de informacion extra que se necesitara.
        */
        public int getNumExtraDataNeded()
        {
            return this.mode.numExtraDataNeeded(this);
        }

        /**
        * <p> Getter.
        * </p>
        * @return copia del nombre del juego.
        */
        public String getGameName()
        {
            String gameNameCopy = new String(this.name);
            return gameNameCopy;
        }

        /**
        * <p> Getter.
        * </p>
        * @return copia del estado del juego.
        */
        public String getStatus()
        {
            String statusCopy = new String(this.status);
            return statusCopy;
        }

        /**
        * <p> Setter, cambia el estado de juego (this.status) por una copia del dado.
        * </p>
        * @param newStatus nuevo estado a setear.
        */
        internal void setStatus(String newStatus)
        {
            this.status = new String(newStatus);
        }

        /**
        * <p> Consulta si el juego esta terminado.
        * </p>
        * @return true si el juego esta terminado, false sino lo esta.
        */
        public bool isFinished()
        {
            return this.status.Equals("Juego Terminado");
        }

        /**
        * <p> Pide al Area de juego las cartas en juego representada en string.
        * </p>
        * @return cartas en juego representadas en string.
        */
        public String cardsInPlayString()
        {
            return this.gameArea.cardsInPlayToString();
        }

        /**
        * <p> Getter.
        * </p>
        * @return opciones de juego segun el modo activo, si es que el juego
        *           no ha terminado, caso contrario ninguna opcion de juego.
        */
        public String[] getPlaysOptions()
        {
            if (!this.status.Equals("Juego Terminado"))
            {
                return this.mode.playsOptions(this);
            }
            else
            {
                return new String[0];
            }
        }

        /**
        * <p> Getter.
        * </p>
        * @return ganadores del juego representado en string.
        */
        public String? getWinners()
        {
            if (this.status.Equals("Juego Terminado"))
            {
                return this.playersGameControl.getWinners().ToString();
            }
            return "No Disponible";
        }

        /**
        * <p> Getter.
        * </p>
        * @return perdedores del juego representado en string.
        */
        public String? getLosers()
        {
            if (this.status.Equals("Juego Terminado"))
            {
                return this.playersGameControl.getLosers().ToString();
            }
            return "No Disponible";
        }

        /**
        * <p> Getter.
        * </p>
        * @return jugadores registrados en su representacion de String.
        */
        public String registeredPlayers()
        {
            return this.playersGameControl.ToString();
        }

        /**
        * <p> Compara this con otro Objeto, para esto compara si son de la misma
        *      clase (DobbleGame) y luego si los dos juegos tienen el mismo nombre
        *      o sus datos son los mismos.
        * </p>
        * @param object objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public override bool Equals(Object? o)
        {
            if (o != null && o.GetType().Equals(this.GetType()))
            {
                DobbleGame dG = (DobbleGame)o;
                return this.name.Equals(dG.getGameName()) || (this.playersGameControl.Equals(dG.playersGameControl) && (this.status == dG.status) && this.mode.Equals(dG.mode) && this.gameArea.Equals(dG.gameArea));
            }
            return false;
        }

        /**
        * <p> Pasa la representacion del juego Dobble a String.
        * </p>
        * @return String en representacion del juego Dobble.
        */
        public override String ToString()
        {
            String gameName = "Nombre del juego: " + this.name;
            String modeName = "\nModo de juego: " + getNameOfMode() + ", en su version: " + getVersionMode();
            String st = "Estado del Juego: " + getStatus();
            String cards = "Cartas en juego:\n" + cardsInPlayString();
            String players = "Jugadores registrados:\n" + registeredPlayers();
            String jump = "\n--------------\n";
            String strFinal = gameName + modeName + jump + st + jump + cards + jump + players;
            if (this.status == "Juego Terminado")
            {
                String winners = "Ganadores:\n" + this.playersGameControl.getWinners();
                String losers = "Perdedores:\n" + this.playersGameControl.getLosers();
                String results = "Resultados Finales:\n" + winners + "\n" + losers;
                strFinal += jump + results;
            }
            return strFinal;
        }
    }
}
