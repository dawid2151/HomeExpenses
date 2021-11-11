using HomeExpenses.DTO;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace HomeExpenses.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid ReceiptId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public double ItemPrice { get; set; }
        public float Ammount { get; set; }
        public double Discount { get; set; }
    }
}
