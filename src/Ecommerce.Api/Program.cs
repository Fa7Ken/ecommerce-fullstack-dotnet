using Ecommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Necessario para as configuracoes do Swagger

var builder = WebApplication.CreateBuilder(args);

// Adiciona o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuracao explicita do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce API", Version = "v1" });
});

var app = builder.Build();

// Configura o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Aponta explicitamente para o arquivo gerado
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API v1");
    });
}

// app.UseHttpsRedirection(); // Comentado para evitar o Warning no terminal local
app.UseAuthorization();
app.MapControllers();

app.Run();