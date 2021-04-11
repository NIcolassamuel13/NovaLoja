using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        [Display(Name = "Identificador")]// altera o que vai estar escrito na tela 
        public int Id { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [DisplayFormat(DataFormatString ="{0:f2}")]
        [Display (Name="Salário Base")]
        public double  BaseSalary { get; set; }
        [Display(Name = "Departamento")]
        public Department Department { get; set; }
        [Display(Name = "Identificador Departamento")]
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;  
        }
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }
        public double TotalSales(DateTime initial, DateTime final)
        { // estou pegando o objeto salesRecord com espressão lambd onde o intervalo
            //seja maio ou igual a data inicial e menor ou igual a data final
            // feito isso eu faço a soma com outra lambd que leva a amount o (valor)
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }





    }
}
