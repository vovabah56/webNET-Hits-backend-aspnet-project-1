using System.Text.Json.Serialization;

namespace Delivery.DB.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    Male,
    Female
}