using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbWithDotNet.MongoCollections;

namespace MongoDbWithDotNet.Controllers;

[ApiController]
public class MongoCollectionController : ControllerBase
{
    private readonly IMongoCollection<Employee> _employeeCollection;

    public MongoCollectionController(IMongoClient mongoClient, IConfiguration configuration)
    {
        var databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");
        var employeeCollection = configuration.GetValue<string>("MongoDbSettings:Collections:EmployeeCollection");
        var database = mongoClient.GetDatabase(databaseName);
        _employeeCollection = database.GetCollection<Employee>(employeeCollection);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create()
    {
        try
        {
            var employee = new Employee { Id = ObjectId.GenerateNewId().ToString(), Name = "John Doe", Age = 30 };

            await _employeeCollection.InsertOneAsync(employee);

            return Ok();
        }
        catch (MongoWriteException ex)
        {
            return BadRequest($"Failed to insert document: {ex.Message}");
        }
    }

    [HttpPost("create-many")]
    public async Task<IActionResult> CreateMany()
    {
        var employees = new List<Employee>
        {
            new Employee { Id = ObjectId.GenerateNewId().ToString(), Name = "Test 1", Age = 25, Designation = "SSE", Salary = 15000 },
            new Employee { Id = ObjectId.GenerateNewId().ToString(), Name = "Test 2", Age = 35, Designation = "Team Leader", Salary = 105000 },
            new Employee { Id = ObjectId.GenerateNewId().ToString(), Name = "Test 3", Age = 35, Designation = "Test", Salary = 151022 },
            new Employee { Id = ObjectId.GenerateNewId().ToString(), Name = "Test 4", Age = 35, Designation = "Demo", Salary = 1500 }
        };
        await _employeeCollection.InsertManyAsync(employees);
        return Ok();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _employeeCollection.Find(Builders<Employee>.Filter.Empty).ToListAsync();
        return Ok(employees);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        var employee = await _employeeCollection.Find(Builders<Employee>.Filter.Eq(p => p.Id, id)).FirstOrDefaultAsync();
        return Ok(employee);
    }

    [HttpPut("replace/{id}")]
    public async Task<IActionResult> Replace([FromRoute] string id)
    {
        var filter = Builders<Employee>.Filter.Eq(p => p.Id, id);
        var employee = new Employee { Id = id, Name = "Updated Name", Age = 40 };
        var result = await _employeeCollection.ReplaceOneAsync(filter, employee);
        return Ok(result);
    }

    [HttpPatch("update-field/{id}")]
    public async Task<IActionResult> UpdateField([FromRoute] string id)
    {
        var filter = Builders<Employee>.Filter.Eq(p => p.Id, id);
        var update = Builders<Employee>.Update.Set(p => p.Name, "Partially Updated Name");
        var result = await _employeeCollection.UpdateOneAsync(filter, update);
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var filter = Builders<Employee>.Filter.Eq(p => p.Id, id);
        var result = await _employeeCollection.DeleteOneAsync(filter);
        //var result = _employeeCollection.DeleteManyAsync(filter);
        return Ok(result);
    }

    [HttpGet("count/estimated")]
    public async Task<IActionResult> EstimatedDocumentCount()
    {
        var count = await _employeeCollection.EstimatedDocumentCountAsync();
        return Ok(count);
    }

    [HttpGet("count")]
    public async Task<IActionResult> CountDocuments()
    {
        var filter = Builders<Employee>.Filter.Gt(p => p.Age, 25);
        var count = await _employeeCollection.CountDocumentsAsync(Builders<Employee>.Filter.Empty);
        return Ok(count);
    }
}