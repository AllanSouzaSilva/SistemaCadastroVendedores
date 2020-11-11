using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        //Injeção de dependencia pra chamar o metodo da classe SalesRecordService
        private readonly SalesRecordsService _salesRecordsService;

        public SalesRecordsController(SalesRecordsService salesRecordsService)
        {
            _salesRecordsService = salesRecordsService;
        }



        //Contrutor

        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }
        //METODO DE BUSCA SIMPLES
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue) {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-mm-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-mm-dd");
            //Chamando o metodo.
            var result = await _salesRecordsService.FindByDateAsync(minDate, maxDate);
            // Tenho que chamar o serviço FindByDate tenho que declarar a dependencia dele aqui 
            return View(result);
        }

        //Metodo de busca agrupada
        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
