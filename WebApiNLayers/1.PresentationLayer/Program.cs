using _2.BusinessLayer;
using _3.DataLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var sqlString = builder.Configuration.GetConnectionString("sqlString");
builder.Services.AddScoped<ProductDL>(c => new ProductDL(sqlString!));
builder.Services.AddScoped<ProductBL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
