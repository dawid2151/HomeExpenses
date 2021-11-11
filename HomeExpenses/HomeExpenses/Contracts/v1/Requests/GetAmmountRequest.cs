using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.Contracts.v1.Requests
{
    public class GetAmmountRequest
    {
        public int Count { get; set; }
        public int Offset { get; set; }

    }
}
