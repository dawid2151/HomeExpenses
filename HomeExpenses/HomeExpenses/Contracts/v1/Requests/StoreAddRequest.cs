using HomeExpenses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.Contracts.v1.Requests
{
    public class StoreAddRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string NIP { get; set; }
    }
}
