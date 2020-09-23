
using System.Collections.Generic;

namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; } 
        /* Os nomes usual ajuda o framework a 
        reconhecer os dados e na hora da conversão de dados http para objeto ele já sabe fazer automáticamente*/
    }
}
