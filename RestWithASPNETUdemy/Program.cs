using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using RestWithASPNETErudio.Business;
using RestWithASPNETErudio.Business.Implementations;
using RestWithASPNETErudio.Repository.Implementations;
using RestWithASPNETErudio.Repository;
using RestWithASPNETUdemy.Model.Context;

var builder = WebApplication.CreateBuilder(args);

// Configura a string de conexão
var connectionString = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Adicionando serviços ao container
builder.Services.AddControllers(); // Adiciona serviços para controladores


// Adiciona o versionamento da API
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); // Versão padrão
    options.AssumeDefaultVersionWhenUnspecified = true; // Assume a versão padrão se não especificada
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Caso queira habilitar redirecionamento de HTTP para HTTPS
// app.UseHttpsRedirection();

app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

app.Run();