using System;

namespace HomeExpenses.Contracts.v1.Requests
{
    public class ReceiptAddRequest
    {
        public DateTime DateTime { get; set; }
        public string PaymentMethod { get; set; }
        public string StoreNIP { get; set; }
        public ProductAddRequest[] Products { get; set; }
    }
}
