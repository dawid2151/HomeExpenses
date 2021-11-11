using HomeExpenses.Domain;
using HomeExpenses.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.Services
{
    public interface IReceiptService
    {
        public Receipt GetById(Guid id);

        public List<Receipt> GetInTimeSpan(DateTime from, DateTime to);
        public List<Receipt> GetAmmount(int offset, int count);
        public bool Add(Receipt receipt);
        public bool Update(Receipt existing, Receipt values);
        public bool Delete(Receipt receipt);
    }
    public class ReceiptService : IReceiptService
    {
        private IMapperService _mapperService;

        public ReceiptService(IMapperService mapperService)
        {
            _mapperService = mapperService;
        }
        public bool Add(Receipt receipt)
        {
            if (receipt is null)
                return false;

            bool added = false;
            using (var context = new HomeExpensesContext())
            {
                var store = context.Stores.SingleOrDefault(x => x.NIP == receipt.StoreNIP);
                var dto = _mapperService.DTOFromDomain(receipt);
                dto.Store = store;
                context.Receipts.Add(dto);
                added = context.SaveChanges() > 0;
            }
            return added;
        }

        public bool Delete(Receipt receipt)
        {
            if (receipt is null)
                return false;

            bool deleted = false;
            using (var context = new HomeExpensesContext())
            {
                var dto = context.Receipts.SingleOrDefault(x => x.Id == receipt.Id);
                if (dto is null)
                    return false;

                context.Receipts.Remove(dto);
                deleted = context.SaveChanges() > 0;
            }
            return deleted;
        }

        public List<Receipt> GetAmmount(int offset, int count)
        {
            List<Receipt> receipts = null;
            using (var context = new HomeExpensesContext())
            {
                var dtos = context.Receipts
                    .Include(x => x.Store)
                    .Include(x => x.Products)
                    .OrderBy(x => x.Id)
                    .Skip(offset)
                    .Take(count)
                    .ToList();
                receipts = dtos.Select(dto => _mapperService.DomainFromDTO(dto)).ToList();
            }
            return receipts;
        }

        public Receipt GetById(Guid id)
        {
            Receipt receipt = null;
            using (var context = new HomeExpensesContext())
            {
                var dto = context.Receipts
                    .Include(x => x.Store)
                    .SingleOrDefault(x => x.Id == id);
                receipt = _mapperService.DomainFromDTO(dto);
            }
            return receipt;
        }

        public List<Receipt> GetInTimeSpan(DateTime from, DateTime to)
        {
            List<Receipt> receipts = null;
            using (var context = new HomeExpensesContext())
            {
                var dtos = context.Receipts
                    .Where(x => x.DateTime >= from && x.DateTime <= to)
                    .Include(x => x.Store)
                    .ToList();
                receipts = dtos.Select(dto => _mapperService.DomainFromDTO(dto)).ToList();
            }

            return receipts;
        }

        public bool Update(Receipt existing, Receipt values)
        {
            bool updated = false;
            using (var context = new HomeExpensesContext())
            {
                var dto = context.Receipts.SingleOrDefault(x => x.Id == existing.Id);
                dto.PaymentMethod = values.PaymentMethod == null ? dto.PaymentMethod : values.PaymentMethod;
                dto.DateTime = values.DateTime == null ? dto.DateTime : values.DateTime;
                updated = context.SaveChanges() > 0;
            }
            return updated;
        }
    }
}
