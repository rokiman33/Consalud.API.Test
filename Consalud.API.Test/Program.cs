using Consalud.Domain;
using Consalud.Domain.Application;
using Consalud.Domain.Application.Services;
using Consalud.Manager.Services;
using Consalud.Manager.Services.Impl;
using Consalud.Model.Dto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<FacturaService>();
builder.Services.AddMvc();

#region "Inyeccion de Dependencias"
builder.Services.AddTransient<IFacturasDB,FacturasDB>();
builder.Services.AddTransient<IReadFacturas, ReadFacturasImpl>();

#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
