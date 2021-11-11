using HomeExpenses.Contracts.v1.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeExpenses.Services
{
    public interface IValidatorService
    {
        public bool ValidateNIP(string nip);
        public bool Validate(Guid guid);
        public bool Validate(ProductAddToReceiptRequest request);
        public bool Validate(StoreAddRequest request);
        public bool Validate(ReceiptAddRequest request);
        public bool Validate(ProductAddRequest request);
    }
    public class ValidatorService : IValidatorService
    {
        private const int CHARACTERS_IN_NIP = 13;
        public bool Validate(StoreAddRequest request)
        {
            return true;
        }

        public bool Validate(ReceiptAddRequest request)
        {
            return true;
        }

        public bool Validate(ProductAddRequest request)
        {
            return true;
        }

        public bool Validate(Guid guid)
        {
            var temp = Guid.Empty;
            if (!Guid.TryParse(guid.ToString(), out temp))
                return false;

            return true;
        }

        public bool Validate(ProductAddToReceiptRequest request)
        {
            return true;
        }

        public bool ValidateNIP(string nip)
        {
            if (string.IsNullOrEmpty(nip))
                return false;

            if (nip.Length != CHARACTERS_IN_NIP)
                return false;

            Regex reg = new Regex(@"\d\d\d-\d\d-\d\d-\d\d\d");
            if (!reg.IsMatch(nip))
                return false;

            return true;
        }
    }
}
