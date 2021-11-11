using System.Collections.Generic;


namespace HomeExpenses.Contracts.v1.Responses
{
    public class ManyStoresResponse
    {
        public string PreviousPage { get; set; }
        public string NextPage { get; set; }
        public List<StoreResponse> Stores { get; set; }
    }
}
