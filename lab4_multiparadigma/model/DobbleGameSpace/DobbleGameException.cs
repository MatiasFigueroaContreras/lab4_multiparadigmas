using System;
using System.Collections.Generic;

namespace model.DobbleGameSpace
{
    public class DobbleGameException : Exception
    {
        private int code;
        public int Code { get { return code; } }

        public DobbleGameException(int code, string message) : base(message)
        {
            this.code = code;
        }
    }
}
