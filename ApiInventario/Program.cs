using ApiInventario.Data;
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
