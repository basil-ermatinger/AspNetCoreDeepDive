using Assignment_04_Binding_Validating_Registration_Info.Modules.Employees.Models;
using Assignment_04_Binding_Validating_Registration_Info.Modules.Registration.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/register", (Registration reg) =>
{
	return $"User {reg.Email} is registered successfully!";
}).WithParameterValidation();

app.MapPost("/register", ([FromBody] Registration reg) =>
{
	return $"User {reg.Email} is registered successfully!";
}).WithParameterValidation();

app.MapPost("/registerWithValidationClasses", ([FromBody] RegistrationWithValidationClasses reg) =>
{
	return $"User {reg.Email} is registered successfully!";
}).WithParameterValidation();

app.Run();
