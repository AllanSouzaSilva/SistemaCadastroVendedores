using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //METODO DE BUSCA SIMPLES
        public IActionResult SimpleSearch()
        {
            return View();
        }

        //Metodo de busca agrupada
        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
