using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HydroponicsService.Models
{
    public class SensorData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; } = new Guid();

        public double Ph { get; set; }
        public double Ec { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
