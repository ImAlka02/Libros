using ApiLibros.Models.Entities;
using ApiLibros.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibrosContext>(x => x.UseMySql("server=204.93.216.11;database=itesrcne_libros;user=itesrcne_libuser;password=8Ex298hJU3Ts", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.29-mariadb")));
builder.Services.AddTransient<LibroRepositorio>();
builder.Services.AddControllers();
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
app.MapControllers();
app.Run();
