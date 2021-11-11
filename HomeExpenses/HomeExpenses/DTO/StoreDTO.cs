using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.DTO
{
    public class StoreDTO
    {
        public string NIP { get; set; } 
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual List<ReceiptDTO> Receipts { get; set; }

    }
}
