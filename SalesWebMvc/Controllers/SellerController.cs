using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellerController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellerController(SellerService sellerService, DepartmentService departmentService)
        {
            //Construtor com injeção de depedencia
            _sellerService = sellerService;
            _departmentService = departmentService; 
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();//Buscando a lista de departamento
            var viewModel = new SellerFormViewModel { Departments = departments };
            /* No Departement está iniciando com uma lista que foi buscada
             * */
            return View(viewModel); //Quando a view for acionada pela primeira vez ja irá retornar uma lista com os objetos populados
        }

        [HttpPost] //
        [ValidateAntiForgeryToken] //Para previnir que a aplicação sofra ataques srsf: Quando alguém aproveitaa sessão de autenticaçãoe envia dados maliciososaproveitando a sua autenticação.
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);//Um parametro para inserir o vendedor no metodo que esta no seller service
            return RedirectToAction(nameof(Index)); //Assim que recarregar a pagina esse return vai redireciona para a Index(Tela Principal)
        }
    }
}
