using System;
using System.Collections.Generic;

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
