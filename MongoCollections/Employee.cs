using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbWithDotNet.MongoCollections;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }
    public int Age { get; set; }
    public string Designation { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string[] Skills { get; set; }
}