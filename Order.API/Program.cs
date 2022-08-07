using Order.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Order.API.Repositories;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
//see this 
builder.Services.AddRefitClient<IBookSDataAccessRepository>().ConfigureHttpClient(c =>
c.BaseAddress = new Uri(builder.Configuration.GetConnectionString("BooksAPI")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddDbContext<OrderingDbContext>( options => 
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<PurchaseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
