using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.Domain
{
    public class Receipt
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string PaymentMethod { get; set; }
        public string StoreNIP { get; set; }
    }
}
