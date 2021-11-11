using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.Contracts.v1.Responses
{
    public class ManyReceiptsResponse
    {
        public string PreviousPage { get; set; }
        public string NextPage { get; set; }
        public List<ReceiptResponse> Reciepts { get; set; }
    }
}
