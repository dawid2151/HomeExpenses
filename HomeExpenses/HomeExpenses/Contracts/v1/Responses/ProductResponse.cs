using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.Contracts.v1.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public Guid ReceiptId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public double ItemPrice { get; set; }
        public float Ammount { get; set; }
        public double Discount { get; set; }
    }
}
