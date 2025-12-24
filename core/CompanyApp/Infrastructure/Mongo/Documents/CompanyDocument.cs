using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CompanyApp.Infrastructure.Mongo.Documents;

public class CompanyDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }
}