using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Department
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
           // não colocar no construtur atributos de Icollections não pode 
        }
        public void AddSeler(Seller seller)
        {
            Sellers.Add(seller);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            //vai prcorrer a lista de seller somando e usando o função de que já estava pronta 
            // do seller que já calculava a s venda em um determinado periodo
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
