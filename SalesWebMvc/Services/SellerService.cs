using SalesWebMvc.Data;
using SalesWebMvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

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
        public Seller FindById(int id)
        {
            return _context.Sellers.Include(obj=>obj.Department).FirstOrDefault(obj => obj.Id==id);
        }
        public void Remove(int id)
        {
            var obj = _context.Sellers.Find(id);
            _context.Sellers.Remove(obj);
            _context.SaveChanges();

        }
    }
}
