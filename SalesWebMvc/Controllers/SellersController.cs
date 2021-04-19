using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Excepitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task<IActionResult> Index()
        {
            //esta operação vai retornar uma lista de seller
            var list = await _sellerService.FinAllAsync();// obs await espera a resposta da chamada assincrona
            return View(list);
        }
        //IActionResult é o tipo de retorno de todas as ações 
        public async Task <IActionResult> Create()
        {
          
            var departments =await  _departmentService.FindAllAsync();
            var viewModel = new  SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost] // é uma anotação dizendo quero usar o metodo post não o get
        [ValidateAntiForgeryToken]// não permite que alguem insira conteudo s maliciosos na seção
        public async Task<IActionResult> Create(Seller seller)
        {

            // isso vai contecer enquanto o usuario não preencher corretamente o formulario
            if (!ModelState.IsValid)
            {
                var departments =  await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }
        public async Task< IActionResult> Delete(int? id) // esse ? indica opcional
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message= "Id not privided" });


            }
            var obj =await  _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            }
            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not privided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not privided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Department> departments =await _departmentService.FindAllAsync();
            SellerFormViewModel ViewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch ( ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
           
        }

        public IActionResult Error(string message) // ação de erro
        {// essa ação de erro não precisa ser assincrona pois não tem acesso a dados 
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };//usado para pegar o idinterno da requisição
            return View(viewModel);

        }














    }
}
