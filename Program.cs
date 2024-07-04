using System.Security.Authentication;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString");

var settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
builder.Services.AddSingleton<IMongoClient>(new MongoClient(settings));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();