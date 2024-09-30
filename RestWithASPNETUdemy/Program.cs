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
    builder.Services.AddMvc(options => 
   { 
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
    })
    .AddXmlSerializerFormatters();

    var filterOptions = new HyperMediaFilterOptions();
    filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
    filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

    builder.Services.AddSingleton(filterOptions);


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
        app.UseSwaggerUI();
    }

    // app.UseHttpsRedirection(); // Caso queira habilitar redirecionamento de HTTP para HTTPS

    app.UseAuthorization();

    // Mapeia os controladores
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