using _07_CodeOrganizationAndDependencyInjection.Endpoints;
using _07_CodeOrganizationAndDependencyInjection.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

builder.Services.AddTransient<IEmployeesRepository, EmployeesRepository>();

WebApplication app = builder.Build();

if(!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler();
}

app.UseStatusCodePages();

app.MapEmployeeEndpoints();

app.Run();
