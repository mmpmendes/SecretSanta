var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedis("cache");

//var username = builder.AddParameter("username", secret: true);
//var password = builder.AddParameter("password", secret: true);

// https://github.com/dotnet/aspire/issues/398
//var dbserver = builder.AddPostgres("dbserver", username, password)
//    .WithPgAdmin();
// só no .net sdk 9
//.WithLifetime(ContainerLifetime.Persistent);

//var db = dbserver.AddDatabase("db");

//var migrationService = builder.AddProject<Projects.SecretSanta_MigrationService>("migration")
//    .WithReference(db)
//    .WaitFor(db);

var apiService = builder.AddProject<Projects.SecretSanta_ApiService>("apiservice");
//.WithReference(db)
//.WaitForCompletion(migrationService);

builder.AddProject<Projects.SecretSanta_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    //.WithReference(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
