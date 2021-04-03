using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services.Excepitions
{
    public class DbCouncurrencyException : ApplicationException
    {
        public DbCouncurrencyException (string message): base(message)
        {
         
        }


    }
}
