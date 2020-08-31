using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Seller { get; set; } = new List<Seller>();

        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Seller.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Seller.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
