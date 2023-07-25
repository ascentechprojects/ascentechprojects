using app.advertise.DataAccess;
using app.advertise.libraries;
using app.advertise.libraries.AppSettings;
using app.advertise.libraries.Middlewares;
using app.advertise.services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.DataProtection;
using Serilog;
using Serilog.Events;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



LibrariesConfiguration.Configure(builder.Services);
RepositoryConfiguration.Configure(builder.Services);
ServiceConfiguration.Configure(builder.Services);


builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo($@"{builder.Configuration.GetSection("Secrets:ProtectorPath").Value}"))
    .SetDefaultKeyLifetime(TimeSpan.FromDays(14));

Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Information()
           .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
           .Enrich.FromLogContext()
           .WriteTo.File(
                path: $@"{builder.Configuration.GetSection("Logging:Path:LogFilePath").Value}\log-{DateTime.Now:yyyyMMdd}.txt",
                rollingInterval: RollingInterval.Day
                )
           .CreateLogger();

builder.Host.UseSerilog();

builder.Configuration.GetSection("ConnectionStrings").Get<DBSettings>();
builder.Services.Configure<FileStorageSetting>(builder.Configuration.GetSection("FileStorage"));

var corsUrls = builder.Configuration.GetSection("Cors:AllowedOrigins").Value.Split(",");
builder.Services.AddCors(options =>
{
    options.AddPolicy("_AdvCORSPolicy", builder =>
    {
        builder.WithOrigins(corsUrls)
               .AllowAnyHeader()
               .WithMethods("GET", "POST")
               .AllowAnyMethod();
    });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSerilogRequestLogging();
app.MapControllers();
app.UseCors("_AdvCORSPolicy");
app.UseMiddleware<RequestHeadersMiddleware>();

app.Run();
