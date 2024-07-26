namespace SharedLibrary;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class SensorDataMongo : SensorDataBase
{
    [BsonId]
    [BsonRepresentation(BsonType.Binary)]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public new Guid Id { get; set; } = Guid.NewGuid();
}
