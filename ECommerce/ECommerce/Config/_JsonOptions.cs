using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Config;

public static class _JsonOptions
{
    public static Action<JsonOptions> ForJsonOptions() => options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    };
}