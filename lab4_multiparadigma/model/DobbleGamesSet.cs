using System;
using System.Collections.Generic;
using model.DobbleGameSpace;

namespace model
{
    public class DobbleGamesSet
    {
        private List<DobbleGame> games = new();
        private List<int> gamesSeconds = new();

        public void add(string gameName, int maxP, string mode, List<string> elements, int numE, int maxC, int seconds)
        {
            DobbleGame game = new(gameName, maxP, mode, elements, numE, maxC);
            if (contains(game))
            {
                throw (new DobbleGamesSetException(400, "El juego ya existe."));
            }
            if(seconds <= 0)
            {
                throw (new DobbleGamesSetException(500, "Cantidad de segundos no valida."));
            }

            games.Add(game);
            gamesSeconds.Add(seconds);
        }


        public bool contains(DobbleGame game)
        {
            return this.games.Contains(game);
        }

        public int getGameIndex(DobbleGame game)
        {
            return games.IndexOf(game) + 1;
        }

        public DobbleGame getGame(int i)
        {
            return games[i - 1];
        }

        public int getGameSeconds(DobbleGame game)
        {
            return this.gamesSeconds[getGameIndex(game)];
        }

        public void setGameSeconds(DobbleGame game, int seconds)
        {
            if(seconds <= 0)
            {
                throw (new DobbleGamesSetException(500, "Cantidad de segundos no valida."));
            }
            this.gamesSeconds[getGameIndex(game)] = seconds;
        }
    }
}
