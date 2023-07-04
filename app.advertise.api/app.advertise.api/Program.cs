using app.advertise.DataAccess;
using app.advertise.libraries;
using app.advertise.libraries.AppSettings;
using app.advertise.libraries.Middlewares;
using app.advertise.services;
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

LibrariesConfiguration.Configure(builder.Services);
RepositoryConfiguration.Configure(builder.Services);
ServiceConfiguration.Configure(builder.Services);

builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {//disable the automatic validation of non-nullable properties
            options.SuppressModelStateInvalidFilter = true;
        }).AddFluentValidation(options =>
        {
            // Validate child properties and root collection elements
            options.ImplicitlyValidateChildProperties = true;
            options.ImplicitlyValidateRootCollectionElements = true;

            // Automatic registration of validators in assembly
            options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }); ;

builder.Configuration.GetSection("ConnectionStrings").Get<DBSettings>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("http://example.com", "https://localhost:7058")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("MyCorsPolicy");
app.UseMiddleware<RequestHeadersMiddleware>();

app.Run();
