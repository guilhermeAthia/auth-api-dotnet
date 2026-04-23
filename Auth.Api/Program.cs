using Auth.Application;
using Auth.Api.Middlewares;
using Auth.Infrastructure;
using Auth.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// Injeção de Dependências das Camadas
// ==========================================
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// ==========================================
// Configurações exclusivas da API (Web)
// ==========================================
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Handler de Exceções Global
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// ==========================================
// Pipeline de Requisições HTTP (Middlewares)
// ==========================================
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Auto-criação do Banco de Dados
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler();

app.Run();
