using Microsoft.EntityFrameworkCore;
using WebAPIOrders.Core.RepositoryContracts;
using WebAPIOrders.Core.ServiceContracts;
using WebAPIOrders.Core.Services;
using WebAPIOrders.Repository;
using WebAPIOrders.Repository.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddTransient<IOrdersService, OrdersService>();
builder.Services.AddTransient<IOrderItemsService, OrderItemsService>();
builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();
builder.Services.AddTransient<IOrderItemsRepository, OrderItemsRepository>();
builder.Services.AddDbContext<ApplicationContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
