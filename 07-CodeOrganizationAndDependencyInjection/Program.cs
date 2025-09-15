using _07_CodeOrganizationAndDependencyInjection.Endpoints;
using _07_CodeOrganizationAndDependencyInjection.Models;
using _07_CodeOrganizationAndDependencyInjection.Results;
using Microsoft.AspNetCore.Mvc;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

WebApplication app = builder.Build();

if(!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler();
}

app.UseStatusCodePages();

app.MapEmployeeEndpoints();

app.Run();
