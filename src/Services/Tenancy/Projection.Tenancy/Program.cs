using Asp.Versioning;
using Asp.Versioning.Conventions;
using Projection.ServiceDefaults;
using Projection.Tenancy.Features.Apis;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();


builder.AddApplicationServices();

builder.Services.AddProblemDetails();

var withApiVersioning = builder.Services.AddApiVersioning(
    options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    }
);

builder.AddDefaultOpenApi(withApiVersioning);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .SetIsOriginAllowed((host) => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});


builder.Services.AddExceptionHandler<HttpGlobalExceptionHandler>();


//builder.Services.AddControllers();
//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();

builder.Services.AddScopedServices();

var app = builder.Build();

app.UseDefaultOpenApi();

//app.UseRouting();

//app.UseAuthorization();

app.UseCors("CorsPolicy");

//var versionSet = app.NewApiVersionSet()
//                            .HasApiVersion(1, 0)
//                            .HasApiVersion(2, 0)
//                            .ReportApiVersions()
//                            .Build();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapDefaultControllerRoute();
//app.MapControllers();
app.MapDefaultEndpoints();

//app.MapGroup("api/v{version:apiVersion}/tenants")
//    .MapTenantsApi()
//    .WithApiVersionSet(versionSet)
//    .MapToApiVersion(1, 0)
//    .WithTags("Tenancy")
//    .RequireAuthorization();

var tenancy = app.NewVersionedApi("Tenants");

tenancy.MapTenantsApi()
    .RequireAuthorization();

Log.Logger = CreateSerilogLogger(builder.Configuration);

Serilog.ILogger CreateSerilogLogger(ConfigurationManager configuration)
{
    string Namespace = "Projection.Accounting";
    string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
    var seqServerUrl = configuration["Serilog:SeqServerUrl"];
    var logstashUrl = configuration["Serilog:LogstashgUrl"];
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("TenancyContext", AppName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
        .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://localhost:8080" : logstashUrl, queueLimitBytes: null)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

app.Run();
