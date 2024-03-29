﻿using System;
using System.Collections.Generic;

namespace model.DobbleGameSpace
{
    public class DobbleGameException : Exception
    {
        /// <summary>
        /// Codigo de la excepcion
        /// </summary>
        private int _code;

        /// <summary>
        /// Codigo de la excepcion a obtener.
        /// </summary>
        public int Code { get { return _code; } }

        /// <summary>
        /// Constructor con el mensaje y el codigo asociado a la excepcion
        /// </summary>
        public DobbleGameException(int code, string message) : base(message)
        {
            this._code = code;
        }
    }
}
