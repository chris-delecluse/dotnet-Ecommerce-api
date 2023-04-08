using ECommerce.Config;
using ECommerce.DataAccess;
using ECommerce.Helpers;
using ECommerce.Repositories;
using ECommerce.Services;

var builder = WebApplication.CreateBuilder(args);

// Custom configuration
GlobalConfig.InitializeConfig(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(GlobalConfig.GetJsonOptions());

builder.Services.AddAuthentication()
    .AddJwtBearer(GlobalConfig.GetTokenValidationOptions());
    //.AddCookie(GlobalConfig.GetCookieAuthenticationOptions());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(GlobalConfig.GetSwaggerGenOptions());

builder.Services.AddSingleton<DatabaseContext>();

builder.Services.AddSingleton<IAuthService, AuthService>();

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

builder.Services.AddSingleton<OrderRepository>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();

builder.Services.AddSingleton<TokenRepository>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<ITokenRepository, TokenRepository>();

builder.Services.AddFilters();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<CustomMiddlewareDemo>();

app.UseHttpsRedirection();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();