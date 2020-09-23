using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;
            //readonly: pra previnir que essa dependencia não possa ser alterada

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); //Para retornar uma lista de vendedores
        }
        public void Insert(Seller obj) // Irá inserir um vendedor com novo cadastro no banco de dados
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
