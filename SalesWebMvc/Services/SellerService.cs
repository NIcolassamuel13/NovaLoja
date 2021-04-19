using SalesWebMvc.Data;
using SalesWebMvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Excepitions;


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
        public async Task<List<Seller>> FinAllAsync()
        {
            return await _context.Sellers.ToListAsync();
        }
        // metodo para inserir um novo vendedor
        public async Task InsertAsync(Seller obj)// tranformando a operação de inserção em assincroma
        {

            _context.Add(obj);// essa operação é feita somente em memoria
            await _context.SaveChangesAsync();// essa operação acessa o banco de dados devo colocar a versão
            // async nela por ela fazer o acesso as banco de dados
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Sellers.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Sellers.FindAsync(id);
                _context.Sellers.Remove(obj);
                await _context.SaveChangesAsync();


            }
            catch (DbUpdateException e)
            {

                throw new IntegritydException("Atenção, existem vendas deste vendedor, não posso deletar!");
            }
            
        }
        public async Task UpdateAsync(Seller obj)
        {
            bool hasany = await _context.Sellers.AnyAsync(x => x.Id == obj.Id);// usar o anyAsync para testar
            if (!hasany)// essa chamada serva para testa se no banco
                //existe alguem com o mesmo Id do objeto
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Sellers.Update(obj);
              await  _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbCouncurrencyException(e.Message);
            }
        }
    }
}
