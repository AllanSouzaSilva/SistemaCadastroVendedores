
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;

namespace SalesWebMvc.Data
{
    public class SalesWebMvcContext : DbContext /*Dbcontext:Um objeto Dbcontext encapsula uam sessã com o banco
                                                 de dados para um determinado modelo de dados (Representados por Dbsets)*/ 
    {
        public SalesWebMvcContext(DbContextOptions<SalesWebMvcContext> options)
            : base(options)
        {
        }
        /* Representa a coleção de entidades de um dado tipo em um contexto. Tipicamente corresponde
         * a uma tabela no banco de dados*/ 
        public DbSet<Department> Department { get; set; } 
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }




    }
}
