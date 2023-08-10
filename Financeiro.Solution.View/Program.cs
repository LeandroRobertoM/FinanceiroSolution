
using Financeiro.Solution.Infra.Data.Configuracao;
using Financeiro.Solution.Infra.Data.Migrations;
using Financeiro.Solution.Infra.Data.Migrations.Context;
using Financeiro.Solution.Infra.Data.Migrations.Extensions;
using Financeiro.Solution.Infra.Data.Migrations.Migrations;
using Financeiro.Solution.Infra.Data.Repositorio;
using Financeiro.Solution.View.Token;
using FinanceiroSolution.Domain.Entidades;
using FinanceiroSolution.Domain.Interfaces.ICategoria;
using FinanceiroSolution.Domain.Interfaces.IDespesa;
using FinanceiroSolution.Domain.Interfaces.InterfaceServicos;
using FinanceiroSolution.Domain.Interfaces.ISistemaFinanceiro;
using FinanceiroSolution.Domain.Interfaces.IUsuarioSistemaFinanceiro;
using FinanceiroSolution.Domain.Interfaces.Servicos;
using FinanceiroSolution.Domain.Servicos;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using Serilog;
using Financeiro.Solution.View.Extensions;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

var configuration = builder.Configuration;

startup.ConfigureServices(builder.Services);
builder.Services.AddControllers();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EntityFramework>(options =>
               options.UseSqlServer(
                   builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<EntityFramework>();


// INTERFACE E REPOSITORIO
builder.Services.AddSingleton<InterfaceCategoria, CategoriaRepository>();
builder.Services.AddSingleton<InterfaceDespesa, DespesaRepository>();
builder.Services.AddSingleton<InterfaceSistemaFinanceiro, SistemaFinanceiroRepository>();
builder.Services.AddSingleton<InterfaceUserSistemaFinanceiro, UsuarioSistemaFinanceiroRepository>();



// SERVIÇO DOMINIO
builder.Services.AddSingleton<ICategoriaServico, CategoriaServico>();
builder.Services.AddSingleton<IDespesaServico, DespesaServico>();
builder.Services.AddSingleton<ISistemaFinanceiroServico, SistemaFinanceiroServico>();
builder.Services.AddSingleton<IUsuarioSistemaFinanceiroServico, UsuarioSistemaFinanceiroServico>();




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(option =>
             {
                 option.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,

                     ValidIssuer = "Teste.Securiry.Bearer",
                     ValidAudience = "Teste.Securiry.Bearer",
                     IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
                 };

                 option.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = context =>
                     {
                         Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                         return Task.CompletedTask;
                     }
                 };
             });



// Add services to the container.


//builder.Services.AddAuthentication();

// Processo geração de LOG

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddControllers();

var app = builder.Build();

app.MigrateDatabase(configuration);

// Configure the HTTP request pipeline testes .
Log.Information("Configuring Swagger passou na program. Verificar..");
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.ConfigSwagger();
    app.UseSwaggerUI();

    // Log depois da configuração do Swagger
    Log.Information("Swagger configuration completedpassou na programpassou na programpassou na program.");
}



var devClient = "http://localhost:4200";

app.UseCors(x =>
x.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
.WithOrigins(devClient));


app.UseHttpsRedirection();
// testes de autenticacao 
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.Run();

