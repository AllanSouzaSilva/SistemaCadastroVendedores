using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Department> FindAll()// Metodo para retornar todos departamentos
        {
            return _context.Department.OrderBy(x => x.Name).ToList();//Retorna a lista ordenarda por nome 
        }
    }
}
