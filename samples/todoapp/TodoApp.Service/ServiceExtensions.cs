// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.Datasync.Server.NSwag;
using CommunityToolkit.Datasync.Server.Swashbuckle;

namespace Microsoft.AspNetCore.Builder;

public static class ServiceExtensions
{
    public static void AddSwaggerSupport(this WebApplicationBuilder builder)
    {
        SwaggerDriver driver = builder.Configuration.GetSwaggerDriver();
        switch (driver)
        {
            case SwaggerDriver.NSwag:
                _ = builder.Services.AddOpenApiDocument(options => options.AddDatasyncProcessor());
                break;
            case SwaggerDriver.Swashbuckle:
                _ = builder.Services.AddEndpointsApiExplorer().AddSwaggerGen(options => options.AddDatasyncControllers());
                break;
            case SwaggerDriver.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(builder), "Invalid SwaggerDriver value");
        }
    }

    public static void UseSwaggerSupport(this WebApplication app)
    {
        SwaggerDriver driver = app.Configuration.GetSwaggerDriver();
        switch (driver)
        {
            case SwaggerDriver.NSwag:
                _ = app.UseOpenApi().UseSwaggerUI();
                break;
            case SwaggerDriver.Swashbuckle:
                _ = app.UseSwagger().UseSwaggerUI();
                break;
            case SwaggerDriver.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(app), "Invalid SwaggerDriver value");
        }
    }

    public static SwaggerDriver GetSwaggerDriver(this IConfiguration configuration)
    {
        string? swaggerDriver = configuration["Swagger:Driver"];
        if (swaggerDriver?.Equals("NSwag", StringComparison.InvariantCultureIgnoreCase) == true)
        {
            return SwaggerDriver.NSwag;
        }
        else if (swaggerDriver?.Equals("Swashbuckle", StringComparison.InvariantCultureIgnoreCase) == true)
        {
            return SwaggerDriver.Swashbuckle;
        }
        else
        {
            return SwaggerDriver.None;
        }
    }

    public enum SwaggerDriver
    {
        None,
        NSwag,
        Swashbuckle
    }
}
