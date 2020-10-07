
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

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
        public void Update(Seller obj)
        {
         //Testando se o ID do objeto já existe no banco, o Any serve pra falar se existe algum registro na banco de dados com a condição que foi colocada.
         //Testando se já tem no banco de dados algum vendedor x cujo o id seja igual ao id do obj. 
            if ( !_context.Seller.Any(x => x.Id == obj.Id))
            {
                //Se não existir (!) Vou retornar uma exception
                throw new KeyNotFoundException("Not Found");
            }
            //Se conter o objeto o ef atualizara o objeto
            
            try //Estou interceptando uma exeção do nivel de acesso a dados  e estou relançando a exeção

            { 
                _context.Update(obj);
                _context.SaveChanges(); //para salvar no banco
            }
            catch (DbUpdateConcurrencyException e){
                /*Exeção a nivel de serviço para segregar as camadas 
                 se uma exeção de nivel de acesso a dados acontecer a minha camada de serviço ela vai lançar uma exeção da camada dela
                ai o controlador no caso o sellerController  vai ter que lidar com exeções da camada de serviço 
                é uma forma de respeitar a arquitetura */
                throw new DbConcurrencyException(e.Message);//Essa mensagem virá do BD
            }
        }

    }
}
