using HomeExpenses.Contracts.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.Services
{
    public interface IUriService
    {
        string GetStoreUri(string nip);
    }
    public class UriService : IUriService
    {
        private readonly string _baseUrl;

        public UriService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public string GetStoreUri(string nip)
        {
            var uri = string.Concat(_baseUrl, Routes.Stores.Get, "&id=", nip);
            return uri;
        }

    }
}
