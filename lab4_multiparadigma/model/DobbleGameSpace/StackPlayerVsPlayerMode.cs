using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model.DobbleGameSpace
{
    internal class StackPlayerVsPlayerMode: Stack
    {
        /**
        * <p> Otorga una lista con las opciones de juego segun el estado en el
        *       que este se encuentra.
        * </p>
        * @param dGame juego Dobble, para obtener el estado de este.
        * @return lista con las opciones de juego.
        */
        public override string[] playsOptions(DobbleGame dGame)
        {
            string status = dGame.getStatus();
            string[] str;

            if (status.Equals("Esperando cartas en mesa"))
            {
                str = new string[1];
                str[0] = "Voltear Cartas";
            }
            else if (status.Equals("Cartas volteadas"))
            {
                str = new string[2];
                str[0] = "Elegir elemento en comun";
                str[1] = "Pasar";
            }
            else
            {
                str = new string[1];
                str[0] = "Siguiente jugada";
            }

            return str;
        }

        /**
        * <p> Permite realizar una jugada, segun el estado del juego y la opcion
        *       ingresada
        * </p>
        * @param dGame juego Dobble para la gestion y control de la jugada.
        * @param option opcion para realizar la jugada.
        * @return estado luego de la jugada realizada.
        */
        public override string play(DobbleGame dGame, string option)
        {
            string status = dGame.getStatus();
            if (dGame.numDobbleCards() + dGame.numCardsInPlay() < 2)
            {
                dGame.finish();
                throw new DobbleGameException(501, "Juego finalizado.");
            }
            if (status.Equals("Esperando cartas en mesa"))
            {
                if (option.Equals("Voltear Cartas"))
                {
                    dGame.addDobbleCardsInPlay(1, 2);
                    return "Cartas volteadas";
                }
            }
            else if (status.Equals("Cartas volteadas"))
            {
                if (option.Equals("Pasar"))
                {
                    dGame.backCardsInPlay();
                    dGame.nextTurn();
                    return "Esperando cartas en mesa";
                }
            }
            else if (option.Equals("Siguiente jugada"))
            {
                return "Esperando cartas en mesa";
            }
            throw new DobbleGameException(600, "La opcion ingresada no es valida.");
        }

        /**
        * <p> Permite realizar una jugada, verificando el estado del juego y 
        *       la opcion ingresada sean acordes con la jugada, y para esto
        *       utiliza la informacion extra otorgada.
        * </p>
        * @param dGame juego Dobble para la gestion y control de la jugada.
        * @param option opcion para realizar la jugada.
        * @param data informacion extra con el elemento que es necesario 
        *               para realizar la jugada.
        * @return estado luego de la jugada realizada.
        */
        public override string play(DobbleGame dGame, string option, string[] data)
        {
            if (dGame.getStatus().Equals("Cartas volteadas"))
            {
                if (option.Equals("Elegir elemento en comun"))
                {
                    string element = data[0];
                    string playerStatus = spotIt(element, dGame);
                    string statusResult = dGame.whoseTurnIsIt() + ": " + playerStatus;
                    dGame.setStatus(playerStatus);
                    pass(dGame);
                    return statusResult;
                }
            }
            throw new DobbleGameException(600, "La opcion ingresada no es valida.");
        }

        /**
        * <p> Inicializa el juego.
        * </p>
        * @param dGame juego Dobble.
        * @return estado luego de iniciar el juego.
        */
        public override string start(DobbleGame dG)
        {
            return "Esperando cartas en mesa";
        }

        /**
        * <p> Consulta si el modo de juego segun una jugada y el estado del juego
        *       necesita informacion extra, para poder ser realizada.
        * </p>
        * @param status estado del juego.
        * @param option opcion de juego.
        * @return Element si es necesario que se ingrese este, o null si no se 
        *           necesita informacion extra.
        */
        public override string? extraDataNeeded(string status, string option)
        {
            if (status.Equals("Cartas volteadas") && option.Equals("Elegir elemento en comun"))
            {
                return "Element";
            }
            else
            {
                return null;
            }
        }

        /**
        * <p> Getter.
        * </p>
        * @return Version del modo de juego "Player vs Player".
        */
        public override string getVersionModeName()
        {
            return "Player VS Player";
        }

        /**
        * <p> Getter.
        * </p>
        * @return 2, ya que el modo de juegor establece que es un jugador contra jugador.
        */
        public override int getMaxPlayers()
        {
            return 2;
        }

        /**
        * <p> Getter.
        * </p>
        * @return 2, ya que se necesitan los dos jugadores.
        */
        public override int getMinPlayers()
        {
            return 2;
        }

        /**
        * <p> Getter.
        * </p>
        * @return 0 ya que no se ncestian jugadores extra a registrar.
        */
        public override int getExtraPlayers()
        {
            return 0;
        }

        /**
        * <p> Compara this con otro Objeto, consultando que sean de la misma clase.
        * </p>
        * @param object objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public override bool Equals(object? o)
        {
            return o != null && o.GetType().Equals(GetType());
        }

        public override string ToString()
        {
            return base.getModeName() + " " + getVersionModeName();
        }
    }
}
