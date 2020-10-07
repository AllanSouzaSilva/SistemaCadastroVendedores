
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public Seller FindById(int id) // Vai receber um int ID e vai retornar um vendedor que possui esse ID. Se o vend não existir ele vai retornar null
        {
            //Igarload que é carregar outros objetos além do objeto já carregado .
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);//Para remover o objeto do dbset 
            _context.SaveChanges();//Para o Entity framework efetiva  la a remoção do vendedor lá no banco.
        }

    }
}
