using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using RestWithASPNETErudio.Business;
using RestWithASPNETErudio.Business.Implementations;
using RestWithASPNETErudio.Repository;
using RestWithASPNETUdemy.Model.Context;
using MySqlConnector;
using EvolveDb;
using Serilog;
using RestWithASPNETErudio.Repository.Generic;
using RestWithASPNETUdemy.Hipermedia.Filters;
using RestWithASPNETErudio.Hypermedia.Enricher;
using RestWithASPNETErudio.Hypermedia.Filters;
using RestWithASPNETUdemy.Hipermedia.Enricher;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configura a string de conexão
var connectionString = builder.Configuration["MySQLConnection:MySQLConnectionString"];
if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException(nameof(connectionString), "The MySQL connection string cannot be null or empty.");
}

builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Caso esteja em desenvolvimento, aplica as migrations com Evolve
if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connectionString);
}

// Configura o CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Adicionando serviços ao container
builder.Services.AddControllers(); // Adiciona serviços para controladores

// Adiciona o versionamento da API
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); // Versão padrão
    options.AssumeDefaultVersionWhenUnspecified = true; // Assume a versão padrão se não especificada
});

// Configura o Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "RESTful API with ASP.NET",
        Description = "This is a simple API built with ASP.NET Core following REST principles",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Madson",
            Url = new Uri("https://linkedin.com/in/madson")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Organiza as operações em grupos usando tags baseadas no controlador
    options.TagActionsBy(api => new[] { api.ActionDescriptor.RouteValues["controller"] ?? "Default" });

    // Exibe a versão da API na URL
    options.EnableAnnotations();

    // Configuração para gerar documentação XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) // Evita erros caso o arquivo XML não exista
    {
        options.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddEndpointsApiExplorer();

// Configura Content Negotiation
builder.Services.AddMvc(options => 
{ 
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
})
.AddXmlSerializerFormatters();

// Configuração HATEOAS
var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
builder.Services.AddSingleton(filterOptions);

// Injeção de dependências
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplemetation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

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
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "RESTful API v1");
        options.DocumentTitle = "RESTful API Documentation";
        options.InjectStylesheet("/swagger-ui/custom.css"); // Adiciona CSS customizado, se houver
        options.InjectJavascript("/swagger-ui/custom.js"); // Adiciona JS customizado, se houver
        options.DisplayRequestDuration(); // Mostra a duração da requisição
    });
}

app.UseCors(); // Importante estar antes do UseAuthorization

// app.UseHttpsRedirection(); // Caso queira habilitar redirecionamento de HTTP para HTTPS

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");

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