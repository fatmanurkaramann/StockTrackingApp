using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using StockTrackingApp.Application;
using StockTrackingApp.Application.Abstraction.Token;
using StockTrackingApp.Infastructure;
using StockTrackingApp.Persistance;
using System.Text;
using StockTrackingApp.Infastructure.Services.Token;
using TokenHandler = StockTrackingApp.Infastructure.Services.Token.TokenHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistanceServices();
builder.Services.AddAplicationServices();
builder.Services.AddScoped<ITokenHandler, TokenHandler>();
builder.Services.InfastructureServices();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //oluþturulacak token deðerinin hangi siteler kullanýr --site
            ValidateIssuer = true, //oluþturulacak tokený kimin daðýttýðýný ifade edeceðimiz alan__bizim api
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true, //üretilecek token deðerini uyg ait deðer olduðunu ifade eden key

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
