using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {//1º criar uma dependencia para sellerservice

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        //construtor para injetar dependecia
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            //esta operação vai retornar uma lista de seller
            var list = _sellerService.FinAll();
            return View(list);
        }
        //IActionResult é o tipo de retorno de todas as ações 
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
           return View(viewModel);
        }

        [HttpPost] // é uma anotação dizendo quero usar o metodo post não o get
        [ValidateAntiForgeryToken]// não permite que alguem insira conteudo s maliciosos na seção
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id) // esse ? indica opcional
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (id == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
