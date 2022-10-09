using log4net.Appender;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApplication4.Data;
using WebApplication4.Data.Repository;
using WebApplication4.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Log.Logger = new LoggerConfiguration().
//    MinimumLevel.Information().
//    WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Minute).
//    CreateLogger();

//builder.Logging.AddSerilog();



builder.Services.AddControllers(
    //options => options.ReturnHttpNotAcceptable = true
    ).AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddLog4Net();

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped(typeof(IUniversityRepository<>), typeof(UniversityRepository<>));

builder.Services.AddDbContext<UniversityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityDB"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
