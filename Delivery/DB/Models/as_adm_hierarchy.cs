namespace Delivery.Data.Models;

public class as_adm_hierarchy
{
    public long Id { get; set; }
    public long? ObjectId { get; set; }
    public long? ParentObjectId { get; set; }
    public long? ChangeId { get; set; }
    public string RegionCode { get; set; }
    public string AreaCode { get; set; }
    public string CityCode { get; set; }
    public string PlaceCode { get; set; }
    public string PlanCode { get; set; }
    public string StreetCode { get; set; }
    public long? PrevId { get; set; }
    public long? NextId { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int IsActive { get; set; }
    public string Path { get; set; }
}