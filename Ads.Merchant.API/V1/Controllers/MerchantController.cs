using Ads.Merchant.API.Services;
using Ads.Merchant.API.V1.Models;
using Ads.Merchant.API.V1.Models.RequestModels;
using Ads.Merchant.API.V1.Models.ResponseModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Merchant.API.V1.Controllers;

[ApiController]
[Route("[controller]")]
public class MerchantController : ControllerBase
{
    private readonly IService _service;
    private readonly ILogger<MerchantController> _logger;

    public MerchantController(IService service,ILogger<MerchantController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    
    [HttpPost("Create")]
    public IActionResult CreateMerchant(MerchantCreateRequestModel requestModel)
    {
        
            API.Models.Merchant merchant = new API.Models.Merchant
            {
                Id = Guid.NewGuid().ToString(),
                Name = requestModel.Name,
                Number = requestModel.Number
                
            };
            return Ok(_service.AddMerchant(merchant));
        
    }
    
    [HttpGet]
    public  IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }
    
    [HttpGet(  "{id}")]
    public  IActionResult GetById(string id)
    {
        _logger.LogInformation("GetById Called");
        
        if (string.IsNullOrWhiteSpace(id))
        {
            _logger.LogError("GetById Whitespace or Null");
            return BadRequest();
        }
        
        var merchant = _service.GetById(id);
        
        if (merchant==null)
        {
            _logger.LogError("Merchant Id NotFound");
            return NotFound();
        }
        
        _logger.LogInformation("GetById Success");
        //var response = new MerchantResponseModel(); bu şekilde yaptığımda null dönüyor
        var response = new MerchantResponseModel().ToResponse(merchant);
        return Ok(response);
        
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult DeleteMerchant(string id) 
    {
        var existingMerchant = _service.GetById(id);
        if (existingMerchant == null)
        {
            return NotFound();
        }

        var success = _service.Delete(id);
        if (!success)
        {
            return BadRequest();
        }

        return NoContent(); // 204 no content
    }
}