using Newtonsoft.Json.Serialization;
using EFakturCallback.Configuration;
using EFakturCallback.Extensions;
using EFakturCallback.Data.Repositories;
using EFakturCallback.Authorizations;
using EFakturCallback.Middleware;
using Serilog;
using Serilog.Events;
using EFakturCallback.Factories;
using EFakturCallback.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHealthChecks().AddCheck<CustomHealthCheckMiddleware>("hc");

builder.Services.AddControllers(options =>
{
    options.ValueProviderFactories.Add(new SnakeCaseQueryValueProviderFactory());
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new SnakeCaseNamingStrategy()
    };
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Logger(x => x
            .Filter.ByIncludingOnly(i => i.Level == LogEventLevel.Information | i.Level == LogEventLevel.Debug)
            .WriteTo.File(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/debug/debug_.log"),
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] ({ClassNameDelimited}) {Message}{NewLine}{Exception}",
                retainedFileCountLimit: 31
            )
        )
        .WriteTo.Logger(x => x
            .Filter.ByIncludingOnly(i => i.Level == LogEventLevel.Error | i.Level == LogEventLevel.Fatal)
            .WriteTo.File(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/error/error_.log"),
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] ({ClassNameDelimited}) {Message}{NewLine}{Exception}",
                retainedFileCountLimit: 31
            )
        ).Enrich.FromLogContext()
        .Enrich.WithPropertyDelimited("ClassName")
        .CreateBootstrapLogger();

var _config = builder.Services.RegisterConfiguration(builder.Configuration);

builder.Services.AddDatabaseConfiguration(_config);
builder.Services.AddScoped<IApiKeyValidation, ApiKeyValidation>();
builder.Services.AddScoped<ApiKeyAuthFilter>();
builder.Services.AddScoped<IEfakturListenerRepository, EFakturListenerRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UsePathBase("/api/kala");
app.UseRouting();

app.MapHealthChecks("/hc");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(options =>
{
    app.MapControllers();
    app.MapDefaultControllerRoute();
});

app.MapControllers();

app.Run();

