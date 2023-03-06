using ExpenseManager.Configuration;
using ExpenseManager.Contracts;
using ExpenseManager.Data;
using ExpenseManager.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("ExpenseManagerConnectionString");

builder.Services.AddDbContext<ExpenseManagerDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
           .AllowAnyOrigin()
           .AllowAnyMethod());
});
    
builder.Host.UseSerilog((ctx, lc)=>lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<ICategoriesRepository, CategoryRepository>();
builder.Services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
