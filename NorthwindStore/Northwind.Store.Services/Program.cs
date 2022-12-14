using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        // Para todos los dominios
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        // Para un origen específico
        //builder.WithOrigins("https://localhost:7009").AllowAnyHeader().AllowAnyMethod();
    });
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("NW");
builder.Services.AddDbContextPool<NWContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
