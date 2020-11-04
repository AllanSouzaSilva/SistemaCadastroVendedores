using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;
        //readonly: pra previnir que essa dependencia não possa ser alterada

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }
        //Tasks: É um objeto que encapsula um processamento assincrona deixando a aplicação muito mais facil 
        //Async: Esse sufixo async é uma recomendção da plataforma c#
        public async Task<List<Department>> FindAllAsync()// Metodo para retornar todos departamentos
        {
            //A Espressão linq ela não é executada ela só prepara a consulta, só será executado quando chamar uma outra coisa que provoque a execução dela que nessecaso é a ToList()
            //Esse ToList é que executa a consulta e retorna o resultado para list é uma operação assincrona
            // O ToList Async é uma chamado do framework que ira fazer uma chamada assincrona
            //await : Para avisar o compilador que essa chamada vai ser assincrona, dessa forma essa execução não vai bloquear a minha app
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();//Retorna a lista ordenarda por nome 
        }
    }
}
