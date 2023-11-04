using Delivery.DTO;

namespace Delivery.Services.Interfaces;

public interface IAddressService
{
    public Task<List<AddressDto>> GetParents(Guid guid);
}