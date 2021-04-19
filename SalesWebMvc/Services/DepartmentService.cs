using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // para usar o async tasck
using Microsoft.EntityFrameworkCore; // para poder usar 'tolistasync'

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {



        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }
        public async Task<List<Department>> FindAllAsync() // transformando a operação asincrona
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();// faz uma chamada assincrona deste tolist

            // a palavra awaite informa o compilaçdor que é uma chamada assincrona tem que colocar 
            //depois do return
        }





    }
}
