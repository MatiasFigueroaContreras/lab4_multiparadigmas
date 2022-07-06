using System;
using System.Collections.Generic;

namespace model.DobbleGameSpace
{
    internal abstract class Stack : Mode
    {

        /**
        * <p> Getter.
        * </p>
        * @return nombre del modo de juego "Stack".
        */
        public string getModeName()
        {
            return "Stack";
        }

        /**
        * <p> Getter.
        * </p>
        * @param dGame juego Dobble en este caso no necesario para saber la
        *               cantidad extra de informacion.
        * @return cantidad de informacion extra que se necesitara (1).
        */
        public int numExtraDataNeeded(DobbleGame dGame)
        {
            return 1;
        }

        /**
        * <p> Permite saber si dado un elemento este se repite en almenos dos
        *       cartas que esten en juego.
        * </p>
        * @param element elemento a contar las ocurrencias.
        * @param dGame juego Dobble que permite obtener el numero de ocurrencias
        *               de un elemento sobre las cartas que estan en juego.
        * @return estado de la accion "SpotIt", si se cumple que el elemento se repite
        *           en almenos dos cartas, "NotSpotIt" si no se cumple.
        */
        protected string spotIt(string element, DobbleGame dG)
        {
            if (dG.elementOccurrencesCardsInPlay(element) >= 2)
            {
                return "SpotIt";
            }
            else
            {
                return "NotSpotIt";
            }
        }

        /**
        * <p> Permite pasar una jugada, dependiendo del estado del juego, en donde
        *       si es "SpotIt", se agregara el puntaje y las cartas correspondiente
        *       al jugador que tiene el turno, y se retiraran las cartas en juego.
        * </p>
        * @param dGame juego Dobble que permite realizar las acciones segun el
        *               estado de este.
        */
        protected void pass(DobbleGame dG)
        {
            if (dG.getStatus().Equals("SpotIt"))
            {
                dG.addCardsInPlayCurrentPlayerTurn();
                dG.addScoreCurrentPlayerTurn(dG.numCardsInPlay());
                dG.clearCardsInPlay();
            }
            else if (dG.getStatus().Equals("NotSpotIt"))
            {
                dG.backCardsInPlay();
            }
            dG.nextTurn();
        }

        public abstract string[] playsOptions(DobbleGame dGame);
        public abstract string play(DobbleGame dGame, string option);
        public abstract string play(DobbleGame dGame, string option, string[] data);
        public abstract string start(DobbleGame dG);
        public abstract string getVersionModeName();
        public abstract string? extraDataNeeded(string status, string option);
        public abstract int getMaxPlayers();
        public abstract int getMinPlayers();
        public abstract int getExtraPlayers();
    }
}
