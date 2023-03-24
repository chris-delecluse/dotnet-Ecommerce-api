using ECommerce.Config;
using ECommerce.DataAccess;
using ECommerce.Repositories;
using ECommerce.Services;

var builder = WebApplication.CreateBuilder(args);

// Custom configuration
GlobalConfig.InitializeConfig(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(GlobalConfig.GetJsonOptions());

builder.Services.AddAuthentication().AddJwtBearer(GlobalConfig.GetTokenValidationOptions());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(GlobalConfig.GetSwaggerGenOptions());

builder.Services.AddSingleton<DatabaseContext>();

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

builder.Services.AddSingleton<OrderRepository>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

