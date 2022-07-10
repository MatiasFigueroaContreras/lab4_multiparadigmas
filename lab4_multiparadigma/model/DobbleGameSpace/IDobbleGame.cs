using System;
using System.Collections.Generic;

namespace model.DobbleGameSpace
{
    /**
     *
     * @author Matias Figueroa Conteras
     */
    internal interface IDobbleGame
    {
        public void start();
        public void play(string option);
        public void play(string option, string[] data);
        public void finish();
        public void register(string name);
        public string? whoseTurnIsIt();
        public int getScore(string name);
        public string getNameOfMode();
        public string getVersionMode();
        public string getStatus();
        public string ToString();
        public bool Equals(object? o);
    }
}
