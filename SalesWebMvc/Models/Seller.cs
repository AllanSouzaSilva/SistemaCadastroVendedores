using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        //O campo name quer dizer que é obrigatório
        [Required(ErrorMessage = "{0} required")]
        //Passando uma mensagem de erro para que o cliente preencha o nome com max 60 e min 3
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0}Name size should de between {2} and {1}")] 
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = ("Enter a valid email"))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        //Assim você customiza o que vai aparecer lá no display
        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirtDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        //O salario tem que ser no min 100 e max 50.000.00 mil
        [Range(100.0, 500000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
        public Seller() { }

        public Seller(int id, string name, string email, DateTime birtDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirtDate = birtDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
