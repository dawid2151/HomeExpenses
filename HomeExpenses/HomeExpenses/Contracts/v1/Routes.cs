
namespace HomeExpenses.Contracts.v1
{
    public static class Routes
    {
        public static class Stores
        {
            public const string GetByName = "getbyname";
            public const string Get = "get";
            public const string GetAmmount = "getammount";
            public const string Add = "add";
            public const string Update = "update";
            public const string Delete = "delete/{nip}";
        }
        public static class Receipts
        {
            public const string Get = "get";
            public const string GetMany = "getmany";
            public const string Add = "add";
            public const string Update = "update";
            public const string Delete = "delete/{id}";
        }
        public static class Products
        {
            public const string GetById = "get";
            public const string GetByReceipt = "getbyreceipt";
            public const string AddToReceipt = "addtoreceipt";
            public const string Update = "upate";
            public const string Delete = "delete";
        }
        public static class Analytics
        {
            public const string SpendingsPerCategory = "spendingspercategory";

        }
    }
}
