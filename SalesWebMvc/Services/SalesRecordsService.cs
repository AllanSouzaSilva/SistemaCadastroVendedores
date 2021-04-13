using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordsService
    {
        // Declarar uma dependencia para con context do EF
        private readonly SalesWebMvcContext _context;
        //readonly: pra previnir que essa dependencia não possa ser alterada

        public SalesRecordsService(SalesWebMvcContext context)
        {
            _context = context;
        }
        //Criação findByDate: Este metodo vai retornar uma lista de sales record e onome da operação vai ser FinByDate
     //A data minima e a data max são opcionais que é feita pelo Ponto de interrogação(?)
        public async Task <List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //Esta operação vai receber as duas datas, data minima e data maxima
            //Aqui em baixo baixo vai a logica para capturar as vendas
            //Pode construir as consulta sobre ele
            //Esse consulta não é executado pela simples definição dela
            var result = from obj in _context.SalesRecord select obj; //Essa declaração ele vai pegaras vendadas do tipo DbSet e construir um objeto result do tipo iQuerible
           
            //acrecentando outros detalhes da consulta
            if (minDate.HasValue)
            {
                //Essa expressão que espresse a minha restrição de data 
                // Eu quero o objeto x talque a date seja menor ou igual a data minima
                result = result.Where(x => x.Date >= minDate.Value);
            }
            //Se foi informado uma data maximo qu foi apturada pelo valor
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            //Isso executa a consulta pra mim e me retorna em forma de lista.
            //Foi feito um join com a tabela de vendedor e com a tabela de departamento e ordenar decrescentementepor data
            return await result
                .Include(x => x.Seller)//Isso aqui faz o join das tabelas
                .Include(x => x.Seller.Department) // Faz o join com a tabela de departamentos 
                .OrderByDescending(x => x.Date) //Esse se refere que vai retornar por data 
                .ToListAsync();
        }
        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }

    }
}
