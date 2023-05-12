
using Financeiro.Solution.Infra.Data.Migrations;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Migrations.Extensions;
using Financeiro.Solution.Infra.Data.Migrations.Migrations;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using FinanceiroSolution.Domain.Servicos;
using FluentMigrator.Runner;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);
builder.Services.AddControllers();


builder.Services.AddControllers();

// INTERFACE E REPOSITORIO
//builder.Services.AddSingleton<ICategoriaServico, CategoriaServico>();




// SERVIÇO DOMINIO
//builder.Services.AddSingleton<CategoriaServico, CategoriaServico>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MigrateDatabase();

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

