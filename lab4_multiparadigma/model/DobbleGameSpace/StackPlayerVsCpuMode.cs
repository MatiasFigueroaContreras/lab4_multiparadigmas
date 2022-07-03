using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model.DobbleGameSpace
{
    internal class StackPlayerVsCpuMode : Stack
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
            if (dGame.numDobbleCards() < 2)
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
                    return "Esperando cartas en mesa";
                }
            }
            else if (option.Equals("Siguiente jugada"))
            {
                return "Esperando cartas en mesa";
            }
            throw new DobbleGameException(600, "La opcion ingresada no es valida.");
        }


        private string cpuPlay(DobbleGame dGame, string playerStatus)
        {
            Random rand = new Random();
            int randNumber = (int)rand.NextInt64(1, dGame.numElements());
            string element = dGame.nthElement(randNumber);
            string cpuStatus = spotIt(element, dGame);
            if (cpuStatus.Equals("SpotIt"))
            {
                dGame.addScorePlayer(dGame.numCardsInPlay(), "CPU");
                if (playerStatus.Equals("NotSpotIt"))
                {
                    dGame.addCardsInPlayPlayer("CPU");
                }
            }
            return cpuStatus;
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
                    string cpuStatus = cpuPlay(dGame, playerStatus);
                    dGame.setStatus(playerStatus);
                    string status = dGame.whoseTurnIsIt() + ": " + playerStatus + ", CPU: " + cpuStatus;
                    pass(dGame);
                    dGame.nextTurn();
                    return status;
                }
            }
            throw new DobbleGameException(600, "La opcion ingresada no es valida.");
        }

        /**
        * <p> Inicializa el juego, agregando a la CPU como jugador.
        * </p>
        * @param dGame juego Dobble para agregar a la CPU como jugador.
        * @return estado luego de iniciar el juego.
        */
        public override string start(DobbleGame dG)
        {
            dG.registerExtra("CPU");
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
        * @return Version del modo de juego "Player vs CPU".
        */
        public override string getVersionModeName()
        {
            return "Player vs CPU";
        }

        /**
        * <p> Getter.
        * </p>
        * @return 1, ya que el modo de juegor establece que es jugador contra CPU.
        */
        public override int getMaxPlayers()
        {
            return 1;
        }

        /**
        * <p> Getter.
        * </p>
        * @return 1, ya que se necesita al jugador que jugara contra la CPU.
        */
        public override int getMinPlayers()
        {
            return 1;
        }

        /**
        * <p> Getter.
        * </p>
        * @return 1 ya que se necesita a la CPU como jugador.
        */
        public override int getExtraPlayers()
        {
            return 1;
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
    }
}
