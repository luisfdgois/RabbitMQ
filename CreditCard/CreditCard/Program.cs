using CreditCard;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.ConfigureConsumerServices(configuration);

var app = builder.Build();

await app.RunAsync();
