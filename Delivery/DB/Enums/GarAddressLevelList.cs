namespace Delivery.DB.Enums;

public class GarAddressLevelList
{
    public List<KeyValuePair<string, string>> Levels = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("Region","Регион"),
        new KeyValuePair<string, string>("AdministrativeArea","Административный район"),
        new KeyValuePair<string, string>("MunicipalArea","Муниципальный район"),
        new KeyValuePair<string, string>("RuralUrbanSettlement","Сельское городское поселение"),
        new KeyValuePair<string, string>("City","Город"),
        new KeyValuePair<string, string>("Locality","Населенный Пункт"),
        new KeyValuePair<string, string>("ElementOfPlanningStructure","Элемент Планировочной Структуры"),
        new KeyValuePair<string, string>("ElementOfRoadNetwork","Элемент Дорожной Сети"),
        new KeyValuePair<string, string>("Land","Земля"),
        new KeyValuePair<string, string>("Building","Здание"),
        new KeyValuePair<string, string>("Room", "Комната"),
        new KeyValuePair<string, string>("RoomInRooms","Комната в комнатах"),
        new KeyValuePair<string, string>("AutonomousRegionLevel","Уровень автономного региона"),
        new KeyValuePair<string, string>("IntracityLevel","Внутригородской уровень"),
        new KeyValuePair<string, string>("AdditionalTerritoriesLevel","Уровень дополнительных территорий"),
        new KeyValuePair<string, string>("LevelOfObjectsInAdditionalTerritories","Уровень Объектов На Дополнительных Территориях"),
        new KeyValuePair<string, string>("CarPlace","Место для машины"),
    };
}