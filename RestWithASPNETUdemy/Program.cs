using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using RestWithASPNETErudio.Business;
using RestWithASPNETErudio.Business.Implementations;
using RestWithASPNETErudio.Repository.Implementations;
using RestWithASPNETErudio.Repository;
using RestWithASPNETUdemy.Model.Context;
using MySqlConnector;
using EvolveDb;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configura a string de conexão
var connectionString = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Caso esteja em desenvolvimento, aplica as migrations com Evolve
if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connectionString);
}

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

// Aplica automaticamente as migrations do Entity Framework Core no início da aplicação
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MySQLContext>();
    dbContext.Database.Migrate();
}

// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Caso queira habilitar redirecionamento de HTTP para HTTPS

app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

app.Run();

void MigrateDatabase(string connectionString)
{
    try
    {
        var evolveConnection = new MySqlConnection(connectionString);
        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };
        evolve.Migrate(); // Aplica as migrações de SQL com Evolve
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed", ex);
        throw;
    }
}