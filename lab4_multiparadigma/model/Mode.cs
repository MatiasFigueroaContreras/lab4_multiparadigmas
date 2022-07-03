using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    /**
     * Interfaz que establece los metodos que tiene que proporcionar un modo de
     *  juego para que este pueda ser utilizado y considerado como tal.
     * @author Matias Figueroa Contreras
     */
    internal interface Mode
    {
        /**
        * <p> Otorga una lista con las opciones de juego.
        * </p>
        * @param dGame juego Dobble, por si necesita informacion de este.
        * @return lista con las opciones de juego.
        */
        public String[] playsOptions(DobbleGame dGame);

        /**
        * <p> Permite realizar una jugada.
        * </p>
        * @param dGame juego Dobble por si se necesita utilizar este para la gestion.
        * @param option opcion para realizar la jugada.
        * @return estado luego de la jugada realizada.
        */
        public String? play(DobbleGame dGame, String option);

        /**
        * <p> Permite realizar una jugada.
        * </p>
        * @param dGame juego Dobble por si se necesita utilizar este para la gestion.
        * @param option opcion para realizar la jugada.
        * @param data informacion extra que es necesaria para realizar la jugada.
        * @return estado luego de la jugada realizada.
        */
        public String? play(DobbleGame dGame, String option, String[] data);

        /**
        * <p> Inicializa el juego, con la construccion que sea necesaria.
        * </p>
        * @param dGame juego Dobble por si se necsita utilizar este para la
        *               inicializacion del juego.
        * @return estado luego de iniciar el juego.
        */
        public String start(DobbleGame dG);

        /**
        * <p> Getter.
        * </p>
        * @return Version del modo de juego.
        */
        public String getVersionModeName();

        /**
        * <p> Getter.
        * </p>
        * @return nombre del modo de juego.
        */
        public String getModeName();

        /**
        * <p> Consulta si el modo de juego segun una jugada y el estado del juego
        *       necesita informacion extra, para poder ser realizada.
        * </p>
        * @param status estado del juego.
        * @param option opcion de juego.
        * @return nombre de la informacion extra necesitada, o null si no se 
        *           necesita informacion extra.
        */
        public String? extraDataNeeded(String status, String option);

        /**
        * <p> Getter.
        * </p>
        * @param dGame juego Dobble por si se necesita para saber cuanta informacion
        *               extra es necesaria.
        * @return cantidad de informacion extra que se necesitara.
        */
        public int numExtraDataNeeded(DobbleGame dGame);

        /**
        * <p> Getter.
        * </p>
        * @return cantidad maxima de jugadores aceptada por el modo de juego.
        */
        public int getMaxPlayers();

        /**
        * <p> Getter.
        * </p>
        * @return cantidad minima de jugadores aceptada por el modo de juego.
        */
        public int getMinPlayers();

        /**
        * <p> Getter.
        * </p>
        * @return cantidad de jugadores extra que necesitara el modo de juego.
        */
        public int getExtraPlayers();

        /**
        * <p> Compara this con otro Objeto.
        * </p>
        * @param object objeto a comparar con this.
        * @return true si son iguales, false si no son iguales.
        */
        public bool Equals(Object? o);
    }
}
