using HomeExpenses.Contracts.v1;
using HomeExpenses.Contracts.v1.Requests;
using HomeExpenses.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeExpenses.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private IMapperService _mapperService;
        private IValidatorService _validatorService;
        private IStoreService _storeService;
        public StoresController(IMapperService mapperService, IValidatorService validatorService, IStoreService storeService)
        {
            _mapperService = mapperService;
            _validatorService = validatorService;
            _storeService = storeService;
        }

        [HttpGet(Routes.Stores.Get)]
        public IActionResult Get([FromQuery] string nip)
        {
            var validated = _validatorService.ValidateNIP(nip);
            if (!validated)
                return CommonResponses.ValidationFailed(this);
            var store = _storeService.GetByNIP(nip);
            if (store is null)
                return CommonResponses.DoesNotExist(this);

            var response = _mapperService.ResponseFromDomain(store);
            return Ok(response);
        }

        [HttpGet(Routes.Stores.GetByName)]
        public IActionResult GetByName([FromQuery] string name)
        {
            var store = _storeService.GetByName(name);
            if (store is null)
                return CommonResponses.DoesNotExist(this);

            var response = _mapperService.ResponseFromDomain(store);
            return Ok(response);
        }

        [HttpGet(Routes.Stores.GetAmmount)]
        public IActionResult GetAmmount([FromQuery] GetAmmountRequest request)
        {
            var stores = _storeService.GetAmmount(request.Offset, request.Count);
            if (stores?.Count == 0)
                return NoContent();

            var response = _mapperService.ResponseFromDomain(stores);

            return Ok(response);
        }

        [HttpPost(Routes.Stores.Add)]
        public IActionResult Post([FromForm] StoreAddRequest value)
        {
            var validated = _validatorService.Validate(value);
            if (!validated)
                return CommonResponses.ValidationFailed(this);

            var existing = _storeService.GetByNIP(value.NIP);
            if (existing != null)
                return CommonResponses.AlreadyExists(this);

            var store = _mapperService.DomainFromRequest(value);
            var added = _storeService.Add(store);
            if (!added)
                return Conflict("Could not add.");

            var response = _mapperService.ResponseFromDomain(store);
            return Ok(response);
        }

        [HttpPut(Routes.Stores.Update)]
        public IActionResult Put([FromForm] StoreAddRequest value)
        {
            if( !_validatorService.Validate(value))
                return CommonResponses.ValidationFailed(this);


            var existing = _storeService.GetByNIP(value.NIP);
            if (existing is null)
                return CommonResponses.DoesNotExist(this);

            var newValues = _mapperService.DomainFromRequest(value);

            var updated = _storeService.Update(existing, newValues);
            if (!updated)
                return Conflict("Could not update.");

            var response = _mapperService.ResponseFromDomain(_storeService.GetByNIP(existing.NIP));
            return Ok(response);
        }

        [HttpDelete(Routes.Stores.Delete)]
        public IActionResult Delete(string nip)
        {
            var existing = _storeService.GetByNIP(nip);
            if (existing is null)
                return BadRequest("Store with provided id does not exist.");

            bool deleted = _storeService.Delete(existing);
            if (!deleted)
                return Conflict("Could not delete store with provided id.");

            return NoContent();
        }
        
    }
}
