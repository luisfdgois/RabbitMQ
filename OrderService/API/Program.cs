using API.GraphQL.Mutations;
using API.GraphQL.Queries;
using Application;
using Domain;
using Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers()
                .AddNewtonsoftJson();

builder.Services.AddGraphQLServer()
                .AddQueryType<OrderQuery>()
                .AddMutationType<OrderMutation>();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDomainProcess();

builder.Services.AddApplicationServices()
                .AddOrderContext(configuration)
                .AddInfrestructureServices(configuration);

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

app.MapGraphQL();

app.Run();