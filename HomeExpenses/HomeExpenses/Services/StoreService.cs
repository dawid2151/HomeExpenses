using HomeExpenses.Domain;
using HomeExpenses.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HomeExpenses.Services
{
    public interface IStoreService
    {
        public Store GetByNIP(string nip);
        public Store GetByName(string name);
        public List<Store> GetAmmount(int offset, int count);
        public bool Add(Store store);
        public bool Delete(Store store);
        public bool Update(Store existing, Store values);
    }
    public class StoreService : IStoreService
    {
        private IMapperService _mapperService;

        public StoreService(IMapperService mapperService)
        {
            _mapperService = mapperService;
        }

        public bool Add(Store store)
        {
            bool added = false;
            using(var context = new HomeExpensesContext())
            {
                var dto = _mapperService.DTOFromDomain(store);
                context.Stores.Add(dto);
                added = context.SaveChanges() > 0;
            }

            return added;
        }

        public bool Delete(Store store)
        {
            bool deleted = false;
            using (var context = new HomeExpensesContext())
            {
                var dto = context.Stores.SingleOrDefault(x => x.NIP == store.NIP);
                context.Stores.Remove(dto);
                deleted = context.SaveChanges() > 0;
            }
            return deleted;
        }

        public List<Store> GetAmmount(int offset, int count)
        {
            List <Store> stores = null;
            using (var context = new HomeExpensesContext())
            {
                var dto = context.Stores.OrderBy(x => x.Name).Skip(offset).Take(count);
                stores = dto.Select(storeDTO => _mapperService.DomainFromDTO(storeDTO)).ToList();
            }
            return stores;
        }

        public Store GetByName(string name)
        {
            Store store = null;
            using (var context = new HomeExpensesContext())
            {
                var dto = context.Stores.SingleOrDefault(x => x.Name == name);
                store = _mapperService.DomainFromDTO(dto);
            }
            return store;
        }

        public Store GetByNIP(string nip)
        {
            Store store = null;
            using (var context = new HomeExpensesContext())
            {
                var dto = context.Stores.SingleOrDefault(x => x.NIP == nip);
                store = _mapperService.DomainFromDTO(dto);
            }
            return store;
        }

        public bool Update(Store existing, Store values)
        {
            bool updated = false;
            using (var context = new HomeExpensesContext())
            {
                var existingDTO = context.Stores.SingleOrDefault(x => x.NIP == existing.NIP);
                existingDTO.Name = values.Name == null ? existingDTO.Name : values.Name;
                existingDTO.Address = values.Address == null ? existingDTO.Address : values.Address;
                updated = context.SaveChanges() > 0;
            }
            return updated;
        }
    }
}
