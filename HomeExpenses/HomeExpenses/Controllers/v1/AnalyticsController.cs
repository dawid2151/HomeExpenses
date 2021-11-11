using HomeExpenses.Contracts.v1;
using HomeExpenses.Domain;
using HomeExpenses.DTO;
using HomeExpenses.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeExpenses.Controllers.v1
{
    [Route("[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private IProductService _productService;
        private IReceiptService _receiptService;

        public AnalyticsController(IProductService productService, IReceiptService receiptService)
        {
            _productService = productService;
            _receiptService = receiptService;
        }

        [HttpGet(Routes.Analytics.SpendingsPerCategory)]
        public IActionResult SpendingsPerCategory([FromQuery] DateTime from, DateTime to)
        {
            List<SpendingEntry> values = new List<SpendingEntry>();
            List<Receipt> receipts = _receiptService.GetInTimeSpan(from, to);
            foreach(Receipt rec in receipts)
            {
                var products = _productService.GetByReceipt(rec.Id);
                foreach(Product p in products)
                {
                    if(values.Where(x => x.category == p.Category).Count() < 1)
                    {
                        values.Add(new SpendingEntry(p.Category, p.ItemPrice * p.Ammount));
                        continue;
                    }
                    values.Where(x=>x.category == p.Category).Single().value += p.ItemPrice * p.Ammount;
                }
            }

            List<SpendingResponse> response = values.Select(x =>
            {
                return new SpendingResponse(x.category, x.value);
            }).ToList();

            return Ok(response);
        }

        class SpendingEntry 
        {
            public SpendingEntry(Category c, double v)
            {
                category = c;
                value = v;
            }

            public Category category { get; set; }
            public double value { get; set; }
        }

        class SpendingResponse
        {
            public SpendingResponse(Category category, double value)
            {
                Category = category.ToString();
                Value = value;
            }

            public string Category { get; set; }
            public double Value { get; set; }
        }


    }
}
