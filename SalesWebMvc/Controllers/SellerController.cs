
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Collections.Generic;

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

        public IActionResult Delete(int? id)
        {
            //Primeiro testa se o id é null
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value); // fazendo busca no banco de dados caso não encontrar vou retornar um not found
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {

            //Primeiro testa se o id é null
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value); // fazendo busca no banco de dados caso não encontrar vou retornar um not found
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            //Testando se o Id existe
            if (id == null)
            {
                return NotFound();
            }
            // testar para ver se o Id  existe no banco de dados ou é nullo
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            //Abrir a tela de edição e povuar a caixa de seleção do departamento
            List<Department> departments =  _departmentService.FindAll();
            //Passando os dados para objeto FormViewModel 
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }
        //Ação edit com metodo post
        [HttpPost]
        [ValidateAntiForgeryToken]
        //O Id da url que esta chegando tem que ser igual ao do vendedor que estou passando no metodo
        public IActionResult Edit(int id, Seller seller)
        {
            //testando se o id do vendedor que esta chegando no metodo for diferente do seller.Id alguma coisa esta errada.
            if (id != seller.Id)
            {
                return BadRequest();
            }
            try { 
            _sellerService.Update(seller);
            // Redirecionar para a pagina Index
            return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }

    }
}
