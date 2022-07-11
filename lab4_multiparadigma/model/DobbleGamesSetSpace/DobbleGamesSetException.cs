using System;
using System.Collections.Generic;

namespace model.DobbleGamesSetSpace
{
    public class DobbleGamesSetException : Exception
    {
        /// <summary>
        /// Codigo de la excepcion
        /// </summary>
        private int code;

        /// <summary>
        /// Codigo de la excepcion a obtener.
        /// </summary>
        public int Code { get { return code; } }

        /// <summary>
        /// Constructor con el mensaje y el codigo asociado a la excepcion
        /// </summary>
        public DobbleGamesSetException(int code, string message) : base(message)
        {
            this.code = code;
        }
    }
}
