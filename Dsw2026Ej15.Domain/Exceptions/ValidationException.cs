using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        //Diccionario Campo, Fallo
        public Dictionary<string, string> Errors { get; }
        public int StatusCode { get; }
        public ValidationException(string propertyName, string errorMessage, int statusCode = 400)
            : base("Ocurrió uno o más errores de validación.")
        {
            StatusCode = statusCode;
            Errors = new Dictionary<string, string> { { propertyName, errorMessage } };
        }
    }
}
