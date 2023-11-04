using Delivery.DTO;
using Delivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers;


[ApiController]
[Route("api/address")]
public class AddressController: ControllerBase
{
    public IAddressService _addressService { get; set; }

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }


    [HttpGet]
    [Route("chain")]
    public async Task<List<AddressDto>> GetParents(Guid guid)
    {
        return  await _addressService.GetParents(guid);
    }
}