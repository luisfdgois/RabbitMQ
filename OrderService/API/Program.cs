using Application;
using Domain;
using Infrastructure;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var logger = new LoggerConfiguration().ReadFrom.Configuration(configuration)
                                      .Enrich.FromLogContext()
                                      .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }))
                .AddControllers()
                .AddNewtonsoftJson();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDomainProcess();

builder.Services.AddApplicationServices()
                .AddOrderContext(configuration)
                .AddRepositories()
                .AddInfrestructureServices(configuration)
                .AddBackgroundService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();