using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SecretSanta.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<DBInitializer>();

builder.AddServiceDefaults();

builder.Services.AddDbContextPool<SecretSantaDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("db"), npgsqlOptions =>
    {
        npgsqlOptions.MigrationsAssembly("SecretSanta.MigrationService");
        npgsqlOptions.EnableRetryOnFailure();
    }));

var app = builder.Build();

app.Run();