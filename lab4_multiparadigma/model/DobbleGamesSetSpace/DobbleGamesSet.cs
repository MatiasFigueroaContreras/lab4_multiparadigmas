using System;
using System.Collections.Generic;
using model.DobbleGameSpace;

namespace model.DobbleGamesSetSpace
{
    public class DobbleGamesSet
    {
        /// <summary>
        /// Lista con el conjunto de juegos Dobble.
        /// </summary>
        public List<DobbleGame> games = new();
        /// <summary>
        /// Lista con los segundos asociados a los juegos Dobble.
        /// </summary>
        private List<int> gamesSeconds = new();

        /// <summary>
        /// Permite crear y agregar un juego al conjunto de juegos.
        /// </summary>
        /// <param name="gameName">Nombre del juego</param>
        /// <param name="maxP">Maximos jugadores a registrar</param>
        /// <param name="mode">Modo de juego</param>
        /// <param name="elements">Lista con los elementos</param>
        /// <param name="numE">Numero de elementos en las cartas</param>
        /// <param name="maxC">Maximas cartas a generar</param>
        /// <param name="seconds">Segundos asociados al juego</param>
        /// <exception cref="DobbleGamesSetException"></exception>
        public void add(string gameName, int maxP, string mode, List<string> elements, int numE, int maxC, int seconds)
        {
            DobbleGame game = new(gameName, maxP, mode, elements, numE, maxC);

            if (contains(game))
            {
                throw new DobbleGamesSetException(400, "El juego ya existe.");
            }
            if (seconds <= 0)
            {
                throw new DobbleGamesSetException(500, "Cantidad de segundos no valida.");
            }

            games.Add(game);
            gamesSeconds.Add(seconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">Juego Dobble a ver si es contenido</param>
        /// <returns></returns>
        public bool contains(DobbleGame game)
        {
            return games.Contains(game);
        }

        /// <summary>
        /// Obtiene el indice de un juego
        /// </summary>
        /// <param name="game">Juego Dobble</param>
        /// <returns>Indice del juego</returns>
        public int getGameIndex(DobbleGame game)
        {
            return games.IndexOf(game) + 1;
        }

        /// <summary>
        /// Obtiene el juego dado un indice (partiendo desde 1).
        /// </summary>
        /// <param name="i">Indice</param>
        /// <returns>Juego Dobble</returns>
        public DobbleGame getGame(int i)
        {
            return games[i - 1];
        }

        /// <summary>
        /// Obtiene los segundos dado un indice (partiendo desde 1).
        /// </summary>
        /// <param name="i">Indice</param>
        /// <returns>Segundos</returns>
        public int getSeconds(int i)
        {
            return gamesSeconds[i - 1];
        }

        /// <summary>
        /// Largo de la lista de juegos/segundos.
        /// </summary>
        /// <returns></returns>
        public int length()
        {
            return games.Count;
        }
    }
}
