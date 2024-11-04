using ApiInventario.Data;
using ApiInventario.Repositorio;
using ApiInventario.Servicio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar DbContext al contenedor de servicios
builder.Services.AddDbContext<InventarioContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("RecursosHumanosDB"),
                     new MySqlServerVersion(new Version(8, 0, 29)))); // Ajusta la versión según corresponda

// Permitir CORS
builder.Services.AddCors(options => { options.AddDefaultPolicy(builder => { builder.WithOrigins("http://localhost:3000") .AllowAnyHeader() .AllowAnyMethod(); }); });



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Implementación de de Interfaces y Servicios

builder.Services.AddScoped<IInventarioRepositorio, InventarioRepositorio>();
builder.Services.AddScoped<IInventarioServicio, InventarioServicio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
