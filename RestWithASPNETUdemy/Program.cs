var builder = WebApplication.CreateBuilder(args);

// Adicionando serviços ao container
// Inclui serviços como o Swagger para documentação da API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    // Habilita o Swagger em ambientes de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Caso queira habilitar redirecionamento de HTTP para HTTPS
// app.UseHttpsRedirection();

app.UseAuthorization();

// Mapeia os controladores (necessário para funcionar com os atributos [HttpGet], [HttpPost], etc.)
app.MapControllers();

app.Run();