using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services.Excepitions
{
    public class IntegritydException : ApplicationException
    {
        public IntegritydException(string message): base(message)
        {
        }
    }
}
