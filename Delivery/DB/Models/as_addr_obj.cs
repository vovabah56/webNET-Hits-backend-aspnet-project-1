namespace Delivery.Data.Models;

public class as_addr_obj
{
    public long Id { get; set; }
    public long ObjectId { get; set; }
    public Guid ObjectGuid { get; set; }
    public long? ChangeId { get; set; }
    public string Name { get; set; }
    public string TypeName { get; set; }
    public string Level { get; set; }
    public int? OperTypeId { get; set; }
    public long? PrevId { get; set; }
    public long? NextId { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int IsActual { get; set; }
    public int IsActive { get; set; }
}