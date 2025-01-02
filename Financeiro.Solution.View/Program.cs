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
using FinanceiroSolution.Domain.Servicos.EmailService.Configuration;
using FinanceiroSolution.Domain.Servicos.EmailService;
using FinanceiroSolution.Domain.Interfaces.IPagamento;
using FinanceiroSolution.Domain.Interfaces.IApplicationUser;
using FinanceiroSolution.Domain.Interfaces.IDespesa.IDespesaRecorrente;

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
builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<EntityFramework>();

// INTERFACE E REPOSITORIO
builder.Services.AddSingleton<InterfaceCategoria, CategoriaRepository>();
builder.Services.AddSingleton<InterfaceDespesa, DespesaRepository>();
builder.Services.AddSingleton<InterfaceDespesaRecorrente, DespesaRecorrenciaRepository>();
builder.Services.AddSingleton<InterfaceSistemaFinanceiro, SistemaFinanceiroRepository>();
builder.Services.AddSingleton<InterfaceUserSistemaFinanceiro, UsuarioSistemaFinanceiroRepository>();
builder.Services.AddSingleton<InterfaceUsuarioCreate, UsuarioCriadorRepository>();
builder.Services.AddSingleton<InterfacePagamento, PagamentoRepository>();
builder.Services.AddSingleton<InterfaceApplicationUser, ApplicationUserRepository>();

// SERVIÇO DOMINIO
builder.Services.AddSingleton<ICategoriaServico, CategoriaServico>();
builder.Services.AddSingleton<IDespesaServico, DespesaServico>();
builder.Services.AddSingleton<IDespesaRecorrenteServico, DespesaRecorrenteServico>();
builder.Services.AddSingleton<ISistemaFinanceiroServico, SistemaFinanceiroServico>();
builder.Services.AddSingleton<IUsuarioSistemaFinanceiroServico, UsuarioSistemaFinanceiroServico>();
builder.Services.AddSingleton<IUsuarioCreateServico, UsuarioCreateServico>();
builder.Services.AddSingleton<IPagamentoServico, PagamentoServico>();

// SERVIÇO DE EMAIL
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddAutoMapper(typeof(Program));


// Configuração OAuth Google
builder.Services.AddScoped<OAuthService>();

// Registrar o OAuthService
builder.Services.AddScoped<OAuthService>();  // Certifique-se de registrar o OAuthService

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddControllers();

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
app.MigrateDatabase(configuration);

// Configure the HTTP request pipeline testes.
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
var prdClient1 = "http://164.163.10.101:8080";
var prdClient2 = "http://techserra.com.br:8080";
var hmlClient = "http://192.168.0.106:8080";

app.UseCors(x =>
{
    x.AllowAnyMethod()
     .AllowAnyHeader()
     .WithOrigins(devClient, prdClient1, prdClient2, hmlClient)
     .AllowCredentials();  // Use this if you need to send cookies or HTTP authentication
});

app.UseHttpsRedirection();
// testes de autenticacao
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
