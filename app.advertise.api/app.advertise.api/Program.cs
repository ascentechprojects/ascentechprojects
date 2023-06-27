using app.advertise.DataAccess;
using app.advertise.libraries;
using app.advertise.libraries.AppSettings;
using app.advertise.libraries.Interfaces;
using app.advertise.libraries.Middlewares;
using app.advertise.services;
using app.advertise.services.Admin;
using app.advertise.services.Admin.Interfaces;

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
        });

builder.Configuration.GetSection("ConnectionStrings").Get<DBSettings>();



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

app.UseMiddleware<RequestHeadersMiddleware>();

app.Run();
