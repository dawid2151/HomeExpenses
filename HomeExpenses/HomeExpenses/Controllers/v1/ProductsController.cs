using HomeExpenses.Contracts.v1;
using HomeExpenses.Contracts.v1.Requests;
using HomeExpenses.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeExpenses.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IMapperService _mapperService;
        private IValidatorService _validatorService;
        private IProductService _productService;

        public ProductsController(IMapperService mapperService,
            IValidatorService validatorService,
            IProductService productService)
        {
            _mapperService = mapperService;
            _validatorService = validatorService;
            _productService = productService;
        }
        // GET: api/<ProductsController>
        [HttpGet(Routes.Products.GetById)]
        public IActionResult Get([FromQuery] Guid id)
        {
            var validated = _validatorService.Validate(id);
            if (!validated)
                return CommonResponses.ValidationFailed(this);

            var existing = _productService.GetById(id);
            if (existing is null)
                return CommonResponses.DoesNotExist(this);

            var response = _mapperService.ResponseFromDomain(existing);

            return Ok(response);
        }

        // GET api/<ProductsController>/5
        [HttpGet(Routes.Products.GetByReceipt)]
        public IActionResult GetByReceipt([FromQuery] Guid receiptId)
        {
            var validated = _validatorService.Validate(receiptId);
            if (!validated)
                return CommonResponses.ValidationFailed(this);

            var products = _productService.GetByReceipt(receiptId);
            if (products is null)
                return CommonResponses.DoesNotExist(this);

            var response = _mapperService.ResponseFromDomain(products);
            return Ok(response);
        }

        // POST api/<ProductsController>
        [HttpPost(Routes.Products.AddToReceipt)]
        public IActionResult Post([FromForm] ProductAddToReceiptRequest request)
        {
            var validated = _validatorService.Validate(request);
            if (!validated)
                return CommonResponses.ValidationFailed(this);

            var product = _mapperService.DomainFromRequest(request);
            if (product?.ReceiptId is null)
                return CommonResponses.DoesNotExist(this);

            var added = _productService.Add(product);
            if (!added)
                return Conflict("Could not add product to specified receipt.");

            var response = _mapperService.ResponseFromDomain(product);
            var uri = String.Format("{0}/{1}?id={2}","~",Routes.Products.GetById,response.Id.ToString());
            return Created(uri, response);
        }

        
        // PUT api/<ProductsController>/5
        [HttpPut(Routes.Products.Update)]
        public IActionResult Put([FromForm] Guid existingId, [FromForm] ProductAddToReceiptRequest request)
        {
            var validated = _validatorService.Validate(request) &&
                _validatorService.Validate(existingId);
            if (!validated)
                return CommonResponses.ValidationFailed(this);

            var existing = _productService.GetById(existingId);
            if (existing is null)
                return CommonResponses.DoesNotExist(this);

            var values = _mapperService.DomainFromRequest(request);

            bool updated = _productService.Update(existing, values);
            if (!updated)
                return Conflict("Could not update values.");

            var response = _mapperService.ResponseFromDomain(_productService.GetById(existingId));

            return Ok(response);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete(Routes.Products.Delete)]
        public IActionResult Delete([FromQuery]Guid id)
        {
            var validated = _validatorService.Validate(id);
            if (!validated)
                return CommonResponses.ValidationFailed(this);

            var product = _productService.GetById(id);
            if (product is null)
                return CommonResponses.DoesNotExist(this);

            bool deleted = _productService.Delete(product);
            if (!deleted)
                return Conflict("Could not delete.");

            return Ok();
        }

        
    }
}
