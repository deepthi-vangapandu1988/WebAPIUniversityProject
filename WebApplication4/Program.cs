using log4net.Appender;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Serilog;
using WebApplication4.Data;
using WebApplication4.Data.Repository;
using WebApplication4.Models;
using WebApplication4.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. ",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var key = builder.Configuration.GetValue<string>("APISettings:JWTSecret");

builder.Services.AddAuthentication(n =>
{
    n.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    n.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Default", n =>
{
    n.RequireHttpsMetadata = false;
    n.SaveToken = true;
    n.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder().
    RequireAuthenticatedUser().AddAuthenticationSchemes("Default").Build();
});
builder.Logging.AddLog4Net();

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped(typeof(IUniversityRepository<>), typeof(UniversityRepository<>));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddDbContext<UniversityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityDB"));
});
builder.Services.AddCors(options => options.AddPolicy("TestPolicy", builder =>
{
    //mention multiple origins to allow
    //builder.WithOrigins("http://example.com", "http://www.contoso.com").AllowAnyMethod().AllowAnyHeader();
    //allow all origins
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    //for more info https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0
}));
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
