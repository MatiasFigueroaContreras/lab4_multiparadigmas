using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model.DobbleGameSpace
{
    internal class DobbleException: Exception
    {
        private int code;
        public int Code { get { return code; } }

        public DobbleException(int code, string message) : base(message)
        {
            this.code = code;
        }
    }
}
