using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using NumberCrunchApi.Data;

var builder = WebApplication.CreateBuilder(args);

var connection = new SqliteConnection("Filename=:memory:");
connection.Open();

// Add services to the container.

builder.Services.AddDbContext<NumberCrunchDbContext>(options => options.UseSqlite(connection));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<NumberCrunchApi.SampleGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
