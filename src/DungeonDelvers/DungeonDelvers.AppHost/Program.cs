var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.DungeonDelvers_ApiService>("apiservice");

builder.AddProject<Projects.DungeonDelvers_Web>("webfrontend")
    .WithReference(apiService);

builder.Build().Run();
