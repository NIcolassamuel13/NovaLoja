using Microsoft.AspNetCore.Mvc;
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
    }
}
