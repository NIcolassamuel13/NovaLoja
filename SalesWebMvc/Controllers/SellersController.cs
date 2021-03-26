using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
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

        //construtor para injetar dependecia
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
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
            return View();
        }

        [HttpPost] // é uma anotação dizendo quero usar o metodo post não o get
        [ValidateAntiForgeryToken]// não permite que alguem insira conteudo s maliciosos na seção
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof (Index));
        }


    }
}
