var builder = DistributedApplication.CreateBuilder(args);

var identity = builder.AddProject<Projects.Projection_Identity>("projection-identity");
var idpHttp = identity.GetEndpoint("http");
var idpHttps = identity.GetEndpoint("https");



builder.AddProject<Projects.Projection_Tenancy>("projection-tenancy");



builder.Build().Run();
