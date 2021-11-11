using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public double ItemPrice { get; set; }
        public float Ammount { get; set; }
        public double Discount { get; set; }
        public virtual ReceiptDTO Receipt { get; set; }
    }

    public enum Category
    {
        Jedzenie,
        Napoje,
        Chemia,
        Komunikacja,

    }
}
