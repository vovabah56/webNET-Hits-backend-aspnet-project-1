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


    public async Task<List<AddressDto>> Search(int parentId, string query)
    {
        var kids = await _context.AsAdmHierarchies.Where(x => x.ParentObjectId == parentId).Select(x => x.ObjectId).ToListAsync();
        if (kids != null)
        {
            
            var AdrList =  _context.AsHouses.Where(x => kids.Contains(x.ObjectId) && x.IsActive == 1).Select(x => new AddressDto
            {
                objectGuid = x.ObjectGuid,
                objectId = x.ObjectId,
                text = x.HouseNumber + " " + x.AddNum2,
                objectLevel =
                    Enum.GetName(typeof(GarAddressLevel), (GarAddressLevel)(9)),
                objectLevelText = Enum.GetName(typeof(GarAddressLevel),
                    (GarAddressLevel)(9))
            }).Concat(_context.AsAddrObjs.Where(x => kids.Contains(x.ObjectId) && x.IsActive == 1).Select(x => new AddressDto
            {
                objectGuid = x.ObjectGuid,
                objectId = x.ObjectId,
                text = x.TypeName + " " + x.Name,
                objectLevel = x.Level,
                objectLevelText = x.Level
            }));
            
            var listKids = AdrList.ToList();
            List<AddressDto> result = new List<AddressDto>();
            foreach (var kid in listKids)
            {
                if (kid.text.Contains(query))
                {
                    kid.objectLevel = Enum.GetName(typeof(GarAddressLevel),
                        (GarAddressLevel)(int.Parse(kid.objectLevel) - 1));
                    kid.objectLevelText = GarAddressLevelList.Levels[int.Parse(kid.objectLevelText) - 1].Value;
                    result.Add(kid);
                }
            }
            return result;
            
            
        }
        
        var ex = new Exception();
        ex.Data.Add(StatusCodes.Status404NotFound.ToString(), "Object not found");
        throw ex;
        
        return new List<AddressDto>();



    }

    

    public async Task<List<AddressDto>> GetParents(Guid guid)
    {
        var objectHouses = await _context.AsHouses.FirstOrDefaultAsync(x => x.ObjectGuid == guid);
        var objectBuild = new as_addr_obj();
        long? id;
        if (objectHouses == null)
        {
            objectBuild = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.ObjectGuid == guid);
            id = objectBuild.ObjectId;
        }
        else
        {
            id = objectHouses.ObjectId;
        }
        

        List<AddressDto> list = new List<AddressDto>();

        var AddInH = await _context.AsAdmHierarchies.FirstOrDefaultAsync(x => x.ObjectId == id);
        if (AddInH == null)
        {
            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status404NotFound.ToString(), "Object in h not found");
            throw ex;
        }
        var path = AddInH.Path;
        
        var listPath = path?.Split(".") ?? throw new Exception("Path is null");

        foreach (var objectId in listPath)
        {
            objectHouses = await _context.AsHouses.FirstOrDefaultAsync(x => x.ObjectId.ToString() == objectId);
            
            if (objectHouses == null)
            {
                objectBuild = await _context.AsAddrObjs.FirstOrDefaultAsync(x => x.ObjectId.ToString() == objectId);
                list.Add(new AddressDto
                {
                    objectGuid = objectBuild.ObjectGuid,
                    objectId = objectBuild.ObjectId,
                    text = objectBuild.TypeName + " " + objectBuild.Name,
                    objectLevel =
                        Enum.GetName(typeof(GarAddressLevel), (GarAddressLevel)(int.Parse(objectBuild.Level) - 1)),
                    objectLevelText = Enum.GetName(typeof(GarAddressLevel),
                        (GarAddressLevel)(int.Parse(objectBuild.Level) - 1))
                });
            }
            else
            {
                list.Add(new AddressDto
                {
                    objectGuid = objectHouses.ObjectGuid,
                    objectId = objectHouses.ObjectId,
                    text = objectHouses.HouseNumber + "соор. " + objectHouses.AddNum1,
                    objectLevel =
                        Enum.GetName(typeof(GarAddressLevel), (GarAddressLevel)(9)),
                    objectLevelText = GarAddressLevelList.Levels[9].Value
                });
            }
         
        }


        return list;

    }
    
    
}