using SalesWebMvc.Data;
using SalesWebMvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    // após criar a classe tenho que declarar ela no servico de dependencia que fica lá no startup
    public class SellerService
    {
        //gerando dependencia para dbContext
        //usando readonly boa pratica para previnir que essa dependecia não possa ser alterada
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }
        // operação findall para retornar uma lista com todos os vendedores
        public List<Seller> FinAll()
        {
            return _context.Sellers.ToList();
        }
        // metodo para inserir um novo vendedor
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
