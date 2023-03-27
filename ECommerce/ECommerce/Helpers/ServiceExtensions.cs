using ECommerce.Helpers.Attributes;

namespace ECommerce.Helpers;

public static class ServiceExtensions
{
    public static void AddFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidateRefreshToken>();
    }
}