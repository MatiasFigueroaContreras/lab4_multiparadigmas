using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class DobbleGamesSetException: Exception
    {
        private int code;
        public int Code { get { return code; } }

        public DobbleGamesSetException(int code, string message) : base(message)
        {
            this.code = code;
        }
    }
}
