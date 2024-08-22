var builder = DistributedApplication.CreateBuilder(args);

var identity = builder.AddProject<Projects.Projection_Identity>("projection-identity")
    .WithExternalHttpEndpoints();

var idpHttp = identity.GetEndpoint("http");
var idpHttps = identity.GetEndpoint("https");



builder.AddProject<Projects.Projection_Tenancy>("projection-tenancy")
    .WithEnvironment("Identity__Url", idpHttps);

builder.Build().Run();
