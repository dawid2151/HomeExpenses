using HomeExpenses.Contracts.v1;
using HomeExpenses.Contracts.v1.Requests;
using HomeExpenses.Contracts.v1.Responses;
using HomeExpenses.Domain;
using HomeExpenses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace HomeExpenses.Services
{
    public interface IMapperService
    {
        public StoreResponse ResponseFromDomain(Store domain);
        public ReceiptResponse ResponseFromDomain(Receipt domain);
        public ProductResponse ResponseFromDomain(Product domain);
        public ManyReceiptsResponse ResponseFromDomain(IEnumerable<Receipt> domain);
        public ManyStoresResponse ResponseFromDomain(IEnumerable<Store> domain);
        public ManyProductsResponse ResponseFromDomain(IEnumerable<Product> domain);
        public Store DomainFromRequest(StoreAddRequest request);
        public Product DomainFromRequest(ProductAddToReceiptRequest request);
        public StoreDTO DTOFromDomain(Store domain);
        public ReceiptDTO DTOFromDomain(Receipt domain);
        public ProductDTO DTOFromDomain(Product domain);
        public Store DomainFromDTO(StoreDTO dto);
        public Product DomainFromDTO(ProductDTO dto);
        public Receipt DomainFromDTO(ReceiptDTO dto);
        public Receipt DomainFromRequest(ReceiptAddRequest request);
    }

    public class MapperService : IMapperService
    {
        public Store DomainFromDTO(StoreDTO dto)
        {
            if (dto is null)
                return null;

            var domain = new Store
            {
                NIP = dto.NIP,
                Name = dto.Name,
                Address = dto.Address
            };
            return domain;
        }
        public Product DomainFromDTO(ProductDTO dto)
        {
            if (dto is null)
                return null;

            Product domain = new Product
            {
                Id = dto.Id,
                ReceiptId = dto.Receipt.Id,
                Name = dto.Name,
                Category = dto.Category,
                Ammount = dto.Ammount,
                ItemPrice = dto.ItemPrice,
                Discount = dto.Discount,
            };

            return domain;
        }
        public Receipt DomainFromDTO(ReceiptDTO dto)
        {
            if (dto is null)
                return null;

            var domain = new Receipt
            {
                Id = dto.Id,
                DateTime = dto.DateTime,
                PaymentMethod = dto.PaymentMethod,
                StoreNIP = dto.Store.NIP
            };

            return domain;
        }

        public Store DomainFromRequest(StoreAddRequest request)
        {
            if (request is null)
                return null;

            var domain = new Store
            {
                NIP = request.NIP,
                Address = request.Address,
                Name = request.Name
            };
            return domain;
        }
        public Receipt DomainFromRequest(ReceiptAddRequest request)
        {
            if (request is null)
                return null;

            var domain = new Receipt
            {
                Id = Guid.NewGuid(),
                DateTime = request.DateTime,
                PaymentMethod = request.PaymentMethod,
                StoreNIP = request.StoreNIP
            };

            //foreach product add product

            return domain;
        }

        public Product DomainFromRequest(ProductAddToReceiptRequest request)
        {
            if (request is null)
                return null;

            Product domain = new Product
            {
                Id = Guid.NewGuid(),
                ReceiptId = request.ReceiptId,
                Category = request.Category,
                Name = request.Name,
                ItemPrice = request.ItemPrice,
                Ammount = request.Ammount,
                Discount = request.Discount
            };

            return domain;
        }

        public StoreDTO DTOFromDomain(Store domain)
        {
            //creation of new entry for database
            //move to some sort of factory
            if (domain is null)
                return null;

            var dto = new StoreDTO
            {
                NIP = domain.NIP,
                Name = domain.Name,
                Address = domain.Address
            };

            return dto;
        }
        public ReceiptDTO DTOFromDomain(Receipt domain)
        {
            //factory
            if (domain is null)
                return null;

            var dto = new ReceiptDTO
            {
                Id = domain.Id,
                DateTime = domain.DateTime,
                PaymentMethod = domain.PaymentMethod
            };

            return dto;
        }

        public ProductDTO DTOFromDomain(Product domain)
        {
            if (domain is null)
                return null;

            ProductDTO dto = new ProductDTO
            {
                Id = Guid.NewGuid(),
                Name = domain.Name,
                Category = domain.Category,
                ItemPrice = domain.ItemPrice,
                Ammount = domain.Ammount,
                Discount = domain.Discount
            };

            return dto;
        }

        public StoreResponse ResponseFromDomain(Store domain)
        {
            if (domain is null)
                return null;

            var response = new StoreResponse
            {
                NIP = domain.NIP,
                Address = domain.Address,
                Name = domain.Name
            };

            return response;
        }
        public ManyStoresResponse ResponseFromDomain(IEnumerable<Store> domain)
        {
            if (domain is null)
                return null;

            var response = new ManyStoresResponse
            {
                PreviousPage = "prev",
                NextPage = "next",
                Stores = domain.Select(store => ResponseFromDomain(store)).ToList()
            };

            return response;
        }
        public ReceiptResponse ResponseFromDomain(Receipt domain)
        {
            if (domain is null)
                return null;

            var response = new ReceiptResponse
            {
                Id = domain.Id,
                DateTime = domain.DateTime,
                PaymentMethod = domain.PaymentMethod,
                StoreNIP = domain.StoreNIP
            };

            return response;
        }
        public ProductResponse ResponseFromDomain(Product domain)
        {
            if (domain is null)
                return null;

            var response = new ProductResponse
            {
                Id = domain.Id,
                ReceiptId = domain.ReceiptId,
                Name = domain.Name,
                Category = domain.Category.ToString(),
                ItemPrice = domain.ItemPrice,
                Ammount = domain.Ammount,
                Discount = domain.Discount
            };

            return response;
        }
        public ManyReceiptsResponse ResponseFromDomain(IEnumerable<Receipt> domain)
        {
            if (domain is null)
                return null;

            var response = new ManyReceiptsResponse
            {
                PreviousPage = "prev",
                NextPage = "next",
                Reciepts = domain.Select(receipt => ResponseFromDomain(receipt)).ToList()
            };

            return response;
        }

        public ManyProductsResponse ResponseFromDomain(IEnumerable<Product> domain)
        {
            if (domain is null)
                return null;

            ManyProductsResponse response = new ManyProductsResponse
            {
                ProductResponses = domain.Select(x => ResponseFromDomain(x)).ToList()
            };

            return response;
        }
    }
}
