using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
