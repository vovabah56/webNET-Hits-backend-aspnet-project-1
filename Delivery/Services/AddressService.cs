using AutoMapper;
using Delivery.Data.Models;
using Delivery.DB;
using Delivery.DB.Enums;
using Delivery.DTO;
using Delivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Services;

public class AddressService : IAddressService
{
    public ApplicationDbContext _context { get; set; }
    public IMapper _mapper { get; set; }

    public AddressService(ApplicationDbContext applicationDbContext, IMapper iMapper)
    {
        _mapper = iMapper;
        _context = applicationDbContext;
    }


    public async Task<List<AddressDto>> GetParents(Guid guid)
    {
        var objectBuild = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.ObjectGuid == guid);
        if (objectBuild == null)
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status404NotFound.ToString(), "Object not found");
            throw ex;
        }
        
        
        List<AddressDto> list = new List<AddressDto>
        {
            new AddressDto
            {
                objectGuid = guid,
                objectId = objectBuild.ObjectId,
                text = objectBuild.TypeName + " " + objectBuild.Name,
                objectLevel = Enum.GetName(typeof(GarAddressLevel), (GarAddressLevel)(int.Parse(objectBuild.Level)-1)) ,
                objectLevelText = Enum.GetName(typeof(GarAddressLevel), (GarAddressLevel)(int.Parse(objectBuild.Level)-1))
            }
        };
            
        var objectInH = await _context.AsAdmHierarchies.FirstOrDefaultAsync(x => x.ObjectId == objectBuild.ObjectId);
        if (objectInH == null)
        {
            list.Reverse();
            return list;
        }
            
        while (objectInH.ParentObjectId != 0)
        {
            objectBuild = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.ObjectId == objectInH.ParentObjectId);
            if (objectBuild.Level != null)
                list.Add(new AddressDto
                {
                    objectGuid = objectBuild.ObjectGuid,
                    objectId = objectBuild.ObjectId,
                    text = objectBuild.TypeName + objectBuild.Name,
                    objectLevel = Enum.GetName(typeof(GarAddressLevel),
                        (GarAddressLevel)(int.Parse(objectBuild.Level) - 1)),
                    objectLevelText = Enum.GetName(typeof(GarAddressLevel),
                        (GarAddressLevel)(int.Parse(objectBuild.Level) - 1))
                });
            objectInH = await _context.AsAdmHierarchies.FirstOrDefaultAsync(x => x.ObjectId == objectBuild.ObjectId);
            
        }

        list.Reverse();
        return list;
    }
    
}