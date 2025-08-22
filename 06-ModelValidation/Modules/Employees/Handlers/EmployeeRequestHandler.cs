using _06_MinimalApi_ModelValidation.Modules.Employees.Models;
using _06_MinimalApi_ModelValidation.Modules.Employees.Repositories;
using System.Text.Json;

namespace _06_MinimalApi_ModelValidation.Modules.Employees.Handlers
{
	public static class EmployeeRequestHandler
	{
		public static List<Employee> HandleGetEmployeesByIds(int[] ids)
		{
			return EmployeeRepository.GetEmployees().Where(e => ids.Contains(e.Id)).ToList();
		}

		public static Employee? HandleGetEmployee(int id)
		{
			return HandleGetEmployeeById(id);
		}

		public static Employee? HandleGetEmployeeById(int id)
		{
			return EmployeeRepository.GetEmployee(id);
		}

		public static string? HandleGetPositionById(int identityNumber)
		{
			return EmployeeRepository.GetEmployee(identityNumber)?.Position;
		}

		public static double? HandleGetSalaryById(int id)
		{
			return EmployeeRepository.GetEmployee(id)?.Salary;
		}

		public static string? HandleGetNameById(int id)
		{
			return EmployeeRepository.GetEmployee(id)?.Name;
		}

		public static string? HandleHelloFromEmployee(int id)
		{
			return $"Hello from {EmployeeRepository.GetEmployee(id)?.Name}";
		}

		public static string HandlePost(Employee employee)
		{
			if(employee is null || employee.Id <= 0)
			{
				return "Employee is not provided or is not valid"; ;
			}

			EmployeeRepository.AddEmployee(employee);

			return ("Employee added successfully");
		}

		public static async Task HandlePut(HttpContext context)
		{
			using StreamReader reader = new StreamReader(context.Request.Body);
			string body = await reader.ReadToEndAsync();
			Employee employee = JsonSerializer.Deserialize<Employee>(body)!;

			bool result = EmployeeRepository.UpdateEmployee(employee);

			if(result)
			{
				context.Response.StatusCode = 200;
				await context.Response.WriteAsync("Employee updated successfully!");
				return;
			}
			else
			{
				await context.Response.WriteAsync("Employee not found");
			}
		}

		public static async Task HandleDelete(HttpContext context)
		{
			int id = int.Parse(context.Request.RouteValues["id"].ToString());

			if(context.Request.Headers["Authorization"] == "frank")
			{
				bool result = EmployeeRepository.DeleteEmployee(id);

				if(result)
				{
					await context.Response.WriteAsync("Employee is deleted successfully.");
				}
				else
				{
					context.Response.StatusCode = 404;
					await context.Response.WriteAsync("Employee not found.");
				}
			}
			else
			{
				context.Response.StatusCode = 401;
				await context.Response.WriteAsync("You are not authorized to delete.");
			}
		}
	}
}
