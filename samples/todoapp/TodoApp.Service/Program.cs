// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.Datasync.Server;
using Microsoft.EntityFrameworkCore;
using TodoApp.Service.Database;

#pragma warning disable IDE0058 // Expression value is never used

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new ApplicationException("DefaultConnection is not set");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
        options.EnableThreadSafetyChecks();
    }
});

builder.Services.AddDatasyncServices();
builder.Services.AddControllers();
builder.AddSwaggerSupport();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.InitializeDatabaseAsync();
}

app.UseHttpsRedirection();
app.UseSwaggerSupport();
app.UseAuthorization();
app.MapControllers();

app.Run();