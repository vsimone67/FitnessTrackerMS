using Microsoft.Extensions.HealthChecks;
using System;

public static class HealthCheckBuilderExtensions
{
    public static HealthCheckBuilder AddUrlCheckIfNotNull(this HealthCheckBuilder builder, string url, TimeSpan cacheDuration)
    {
        if (!string.IsNullOrEmpty(url))
        {
            builder.AddUrlCheck(url, cacheDuration);
        }

        return builder;
    }
}