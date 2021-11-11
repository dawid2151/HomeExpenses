using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.DTO
{
    public class ReceiptDTO
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string PaymentMethod { get; set; }
        public virtual StoreDTO Store { get; set; }
        public virtual ICollection<ProductDTO> Products { get; set; }

    }

}
