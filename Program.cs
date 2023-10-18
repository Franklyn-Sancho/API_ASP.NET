using Microsoft.EntityFrameworkCore;
using MyApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Adicione os serviços necessários para usar controladores
builder.Services.AddControllers();

// Configura o uso do SQLite
builder.Services.AddDbContext<MyApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MyApiContext")));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

