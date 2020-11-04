
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();//Buscando a lista de departamento
            var viewModel = new SellerFormViewModel { Departments = departments };
            /* No Departement está iniciando com uma lista que foi buscada
             * */
            return View(viewModel); //Quando a view for acionada pela primeira vez ja irá retornar uma lista com os objetos populados
        }

        [HttpPost] //
        [ValidateAntiForgeryToken] //Para previnir que a aplicação sofra ataques srsf: Quando alguém aproveitaa sessão de autenticaçãoe envia dados maliciososaproveitando a sua autenticação.
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)//Testa pra ver se o modelo foi validado
            {
                var departments = await _departmentService.FindAllAsync();
                var ViewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                //Vai retornar pra mesma view enaqunto o usuário não preencher corretamente o formulário
                return View(ViewModel);
            }
            await _sellerService.InsertAsync(seller);//Um parametro para inserir o vendedor no metodo que esta no seller service
            return RedirectToAction(nameof(Index)); //Assim que recarregar a pagina esse return vai redireciona para a Index(Tela Principal)
        }

        public async Task<IActionResult> Delete(int? id)
        {
            //Primeiro testa se o id é null
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value); // fazendo busca no banco de dados caso não encontrar vou retornar um not found
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        //Versão post
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {

            //Primeiro testa se o id é null
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value); // fazendo busca no banco de dados caso não encontrar vou retornar um not found
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            //Testando se o Id existe
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            // testar para ver se o Id  existe no banco de dados ou é nullo
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            //Abrir a tela de edição e povuar a caixa de seleção do departamento
            List<Department> departments = await _departmentService.FindAllAsync();
            //Passando os dados para objeto FormViewModel 
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }
        //Ação edit com metodo post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        //O Id da url que esta chegando tem que ser igual ao do vendedor que estou passando no metodo
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)//Testa pra ver se o modelo foi validado
            {
                var departments = await _departmentService.FindAllAsync();
                var ViewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                //Vai retornar pra mesma view enaqunto o usuário não preencher corretamente o formulário
                return View(ViewModel);
            }
            //testando se o id do vendedor que esta chegando no metodo for diferente do seller.Id alguma coisa esta errada.
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
               await _sellerService.UpdateAsync(seller);
                // Redirecionar para a pagina Index
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }
        public IActionResult Error(string message)
        {
            //Essa ação de erro não prescisa ser assincrona porque ela não tem nenhum acesso a dados
            var viewModel = new ErrorViewModel
            {
                //O atributo dele vai ser a mensagem que esse metodo vai receber
                Message = message,
                //Um macete para pegar o id interno da requisição ?? Operador de colsença nula
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
