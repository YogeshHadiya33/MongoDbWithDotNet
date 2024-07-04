using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbWithDotNet.MongoCollections;

namespace MongoDbWithDotNet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MongoFiltersController : ControllerBase
{
    private readonly IMongoCollection<Employee> _employeeCollection;

    public MongoFiltersController(IMongoClient mongoClient, IConfiguration configuration)
    {
        var databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");
        var employeeCollection = configuration.GetValue<string>("MongoDbSettings:Collections:EmployeeCollection");
        var database = mongoClient.GetDatabase(databaseName);
        _employeeCollection = database.GetCollection<Employee>(employeeCollection);
    }

    [HttpGet("eq")]
    public async Task<IActionResult> Equal(string name)
    {
        var filter = Builders<Employee>.Filter.Eq(emp => emp.Name, name);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("ne")]
    public async Task<IActionResult> NotEqual(string name)
    {
        var filter = Builders<Employee>.Filter.Ne(emp => emp.Name, name);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("gt")]
    public async Task<IActionResult> GreaterThan(int age)
    {
        var filter = Builders<Employee>.Filter.Gt(emp => emp.Age, age);
        //var filter = Builders<Employee>.Filter.Gt(emp => emp.DateOfBirth, DateTime.Now);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("gte")]
    public async Task<IActionResult> GreaterThanOrEqual(int age)
    {
        var filter = Builders<Employee>.Filter.Gte(emp => emp.Age, age);
        //var filter = Builders<Employee>.Filter.Gte(emp => emp.DateOfBirth, DateTime.Now);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("lt")]
    public async Task<IActionResult> LessThan(int age)
    {
        var filter = Builders<Employee>.Filter.Lt(emp => emp.Age, age);
        //var filter = Builders<Employee>.Filter.Lt(emp => emp.DateOfBirth, DateTime.Now);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("lte")]
    public async Task<IActionResult> LessThanOrEqual(int age)
    {
        var filter = Builders<Employee>.Filter.Lte(emp => emp.Age, age);
        //var filter = Builders<Employee>.Filter.Lte(emp => emp.DateOfBirth, DateTime.Now);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("in")]
    public async Task<IActionResult> In([FromQuery] string[] names)
    {
        var filter = Builders<Employee>.Filter.In(emp => emp.Name, names);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("nin")]
    public async Task<IActionResult> NotIn([FromQuery] string[] names)
    {
        var filter = Builders<Employee>.Filter.Nin(emp => emp.Name, names);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("and")]
    public async Task<IActionResult> And(string name, int age)
    {
        var filter = Builders<Employee>.Filter.And(
            Builders<Employee>.Filter.Eq(emp => emp.Name, name),
            Builders<Employee>.Filter.Eq(emp => emp.Age, age));
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("or")]
    public async Task<IActionResult> Or(string name, int age)
    {
        var filter = Builders<Employee>.Filter.Or(
            Builders<Employee>.Filter.Eq(emp => emp.Name, name),
            Builders<Employee>.Filter.Eq(emp => emp.Age, age));
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("exists")]
    public async Task<IActionResult> Exists(string fieldName)
    {
        var filter = Builders<Employee>.Filter.Exists(fieldName);
        // or
        // var filter = Builders<Employee>.Filter.Exists(x=>x.Designation);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("type")]
    public async Task<IActionResult> Type(string fieldName, BsonType bsonType)
    {
        var filter = Builders<Employee>.Filter.Type(fieldName, bsonType);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("regex")]
    public async Task<IActionResult> Regex()
    {
        var pattern = "^J"; // Names starting with 'J'
        //var pattern = "n$"; // Names ending with 'n'
        //var pattern = ".*[aei].*"; // Names containing 'a', 'e', or 'i'
        //..... and many more 

        var filter = Builders<Employee>.Filter.Regex(emp => emp.Name, new BsonRegularExpression(pattern));
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("all")]
    public async Task<IActionResult> All([FromQuery] string[] skills)
    {
        var filter = Builders<Employee>.Filter.All(emp => emp.Skills, skills);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("elemMatch")]
    public async Task<IActionResult> ElemMatch(string skill)
    {
        var filter = Builders<Employee>.Filter.ElemMatch(emp => emp.Skills, skill);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }

    [HttpGet("size")]
    public async Task<IActionResult> Size(int size)
    {
        var filter = Builders<Employee>.Filter.Size(emp => emp.Skills, size);
        var results = await _employeeCollection.Find(filter).ToListAsync();
        return Ok(results);
    }
}