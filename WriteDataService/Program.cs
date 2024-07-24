using HydroponicsService.Services;

using MongoDB.Driver;

using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/hydroponics-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add Serilog to the application
builder.Logging.AddSerilog();
builder.Services.AddSingleton(Log.Logger);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MongoDB
var mongoConnectionString = builder.Environment.IsDevelopment() ?
    builder.Configuration.GetConnectionString("MongoDBLocal") :
    builder.Configuration.GetConnectionString("MongoDB");

//TO DO - to throw exception if mongo connection not stablished
var mongoClient = new MongoClient(mongoConnectionString);
var database = mongoClient.GetDatabase("SensorsMeasurentsDB");
builder.Services.AddSingleton(database);

// Register services
builder.Services.AddScoped<ISensorDataService, SensorDataService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

Log.CloseAndFlush();
