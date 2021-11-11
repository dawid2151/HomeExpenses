using System;
using System.Collections.Generic;

namespace HomeExpenses.Contracts.v1.Responses
{
    public class ReceiptResponse
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string PaymentMethod { get; set; }
        public string StoreNIP { get; set; }
        public List<ProductResponse> Products { get; set; }
    }
}
