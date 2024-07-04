using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDbWithDotNet.MongoCollections;

namespace MongoDbWithDotNet.Controllers;

[ApiController]
public class MongoDatabaseController : ControllerBase
{
    private readonly IMongoDatabase _database;

    public MongoDatabaseController(IMongoClient mongoClient, IConfiguration configuration)
    {
        var databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");
        _database = mongoClient.GetDatabase(databaseName);
    }

    [HttpPost("create-collection/{name}")]
    public async Task<IActionResult> CreateCollection(string name)
    {
        try
        {
            await _database.CreateCollectionAsync(name);
            return Ok($"Collection '{name}' created successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to create collection '{name}'. Error: {ex.Message}");
        }
    }

    [HttpDelete("drop-collection/{name}")]
    public async Task<IActionResult> DropCollection(string name)
    {
        try
        {
            await _database.DropCollectionAsync(name);
            return Ok($"Collection '{name}' dropped successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to drop collection '{name}'. Error: {ex.Message}");
        }
    }

    [HttpGet("list-collections")]
    public async Task<IActionResult> ListCollections()
    {
        try
        {
            var cursor = await _database.ListCollectionNamesAsync();
            var collectionNames = await cursor.ToListAsync();
            return Ok(collectionNames);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to retrieve collection names. Error: {ex.Message}");
        }
    }

    [HttpPost("rename-collection/{oldName}/{newName}")]
    public async Task<IActionResult> RenameCollection(string oldName, string newName)
    {
        try
        {
            await _database.RenameCollectionAsync(oldName, newName);
            return Ok($"Collection '{oldName}' renamed to '{newName}' successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to rename collection '{oldName}' to '{newName}'. Error: {ex.Message}");
        }
    }

    [HttpPost("get-collection/{collectionName}")]
    public IActionResult GetCollection(string collectionName)
    {
        var collection = _database.GetCollection<Employee>(collectionName);
        return Ok();
    }
}