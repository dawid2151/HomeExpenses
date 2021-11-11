using HomeExpenses.Contracts.v1;
using HomeExpenses.Contracts.v1.Requests;
using HomeExpenses.Services;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeExpenses.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private IMapperService _mapperService;
        private IValidatorService _validatorService;
        private IReceiptService _receiptService;
        private IStoreService _storeService;
        private IProductService _productService;


        public ReceiptsController(IMapperService mapperService, 
            IValidatorService validatorService,
            IReceiptService recieptService,
            IStoreService storeService,
            IProductService productService)
        {
            _mapperService = mapperService;
            _validatorService = validatorService;
            _receiptService = recieptService;
            _storeService = storeService;
            _productService = productService;
        }
        // GET: api/<RecieptsController>
        [HttpGet(Routes.Receipts.Get)]
        public IActionResult GetById([FromQuery] Guid Id)
        {
            var reciept = _receiptService.GetById(Id);
            if (reciept is null)
                return CommonResponses.DoesNotExist(this);

            var products = _productService.GetByReceipt(reciept.Id);

            var response = _mapperService.ResponseFromDomain(reciept);
            response.Products = _mapperService.ResponseFromDomain(products).ProductResponses;

            return Ok(response);
        }

        // GET api/<RecieptsController>/5
        [HttpGet(Routes.Receipts.GetMany)]
        public IActionResult GetMany([FromQuery] GetAmmountRequest request)
        {
            var receipts = _receiptService.GetAmmount(request.Offset, request.Count);
            if (receipts?.Count == 0)
                return NoContent();

            var response = _mapperService.ResponseFromDomain(receipts);

            return Ok(response);
        }

        // POST api/<RecieptsController>
        [HttpPost(Routes.Receipts.Add)]
        public IActionResult Post([FromForm] ReceiptAddRequest value)
        {
            if (!_validatorService.Validate(value))
                return CommonResponses.ValidationFailed(this);

            var store = _storeService.GetByNIP(value.StoreNIP);
            if (store is null)
                return CommonResponses.DoesNotExist(this);

            var receipt = _mapperService.DomainFromRequest(value);
            bool added = _receiptService.Add(receipt);
            if (!added)
                return Conflict("Could not add.");

            var response = _mapperService.ResponseFromDomain(receipt);

            return Ok(response);
        }

        // PUT api/<RecieptsController>/5
        [HttpPut(Routes.Receipts.Update)]
        public IActionResult Put([FromForm] Guid ExistingId, [FromForm] ReceiptAddRequest request)
        {
            if ( !_validatorService.Validate(request))
                return CommonResponses.ValidationFailed(this);

            var existing = _receiptService.GetById(ExistingId);
            if (existing is null)
                return CommonResponses.DoesNotExist(this);

            var values = _mapperService.DomainFromRequest(request);
            bool updated = _receiptService.Update(existing, values);
            if (!updated)
                return Conflict("Could not update values.");

            var response = _mapperService.ResponseFromDomain(_receiptService.GetById(existing.Id));

            return Ok(response);

        }

        // DELETE api/<RecieptsController>/5
        [HttpDelete(Routes.Receipts.Delete)]
        public IActionResult Delete(Guid id)
        {
            var receipt = _receiptService.GetById(id);
            if (receipt is null)
                return CommonResponses.DoesNotExist(this);

            bool deleted = _receiptService.Delete(receipt);
            if (!deleted)
                return Conflict("Could not delete.");

            return Ok();
        }

    }
}
