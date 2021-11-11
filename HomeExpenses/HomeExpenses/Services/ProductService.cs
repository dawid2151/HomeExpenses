using HomeExpenses.Domain;
using HomeExpenses.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.Services
{
    public interface IProductService
    {
        Product GetById(Guid id);
        List<Product> GetByReceipt(Guid receipt);
        bool Add(Product product);
        bool Update(Product existing, Product values);
        bool Delete(Product product);
    }
    public class ProductService : IProductService
    {
        private IMapperService _mapperService;

        public ProductService(IMapperService mapperService)
        {
            _mapperService = mapperService;
        }

        public bool Add(Product product)
        {
            var dto = _mapperService.DTOFromDomain(product);
            bool added = false;
            using (var context = new HomeExpensesContext())
            {
                var receipt = context.Receipts.SingleOrDefault(x => x.Id == product.ReceiptId);
                dto.Receipt = receipt;
                context.Products.Add(dto);
                added = context.SaveChanges() > 0;
            }

            return added;
        }

        public bool Delete(Product product)
        {
            if (product is null)
                return false;

            bool deleted = false;
            using(var context = new HomeExpensesContext())
            {
                var dto = context.Products.SingleOrDefault(x => x.Id == product.Id);
                context.Products.Remove(dto);
                deleted = context.SaveChanges() > 0;
            }

            return deleted;
        }

        public Product GetById(Guid id)
        {
            ProductDTO dto = null;
            using(var context = new HomeExpensesContext())
            {
                dto = context.Products.SingleOrDefault(x => x.Id == id);
            }
            var domain = _mapperService.DomainFromDTO(dto);

            if (domain is null)
                return null;

            return domain;
        }

        public List<Product> GetByReceipt(Guid receipt)
        {
            List<ProductDTO> dto = null;
            using (var context = new HomeExpensesContext())
            {
                dto = context.Products
                    .Include(x=> x.Receipt)
                    .Where(x => x.Receipt.Id == receipt)
                    .ToList();
            }
            if (dto is null)
                return null;

            var domain = dto.Select(x => _mapperService.DomainFromDTO(x)).ToList();
            return domain;
        }

        public bool Update(Product existing, Product values)
        {
            bool updated = false;
            using (var context = new HomeExpensesContext())
            {
                var dto = context.Products.SingleOrDefault(x => x.Id == existing.Id);
                var receiptDto = context.Receipts.SingleOrDefault(x => x.Id == existing.ReceiptId);
               // dto.Category = values.Category ==  ? dto.Category : values.Category;
                dto.Name = values.Name == null ? dto.Name : values.Name;
                // dto.ItemPrice = values.ItemPrice == null ? dto.ItemPrice : values.ItemPrice;

                updated = context.SaveChanges() > 0;
            }

            return updated;
        }
    }
}
