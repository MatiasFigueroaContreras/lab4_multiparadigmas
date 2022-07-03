using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    /**
     *
     * @author Matias Figueroa Conteras
     */
    internal interface IDobbleGame
    {
        public bool start();
        public bool play(String option);
        public bool play(String option, String[] data);
        public void finish();
        public void register(String name);
        public String whoseTurnIsIt();
        public int getScore(String name);
        public String getNameOfMode();
        public String getVersionMode();
        public String getStatus();
        public String ToString();
        public bool Equals(Object? o);
    }
}
