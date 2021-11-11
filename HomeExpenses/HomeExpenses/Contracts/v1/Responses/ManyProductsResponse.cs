using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.Contracts.v1.Responses
{
    public class ManyProductsResponse
    {
        public List<ProductResponse> ProductResponses { get; set; }
    }
}
