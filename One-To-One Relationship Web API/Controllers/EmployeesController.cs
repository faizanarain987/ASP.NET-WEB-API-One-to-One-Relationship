using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using Test.Data;
using Test.Models;
using Test.Models.Enitities;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("InsertAnEmployee")]
        public IActionResult AddEmployee(EmployeeDTO employeeDTO)
        {
            var employee = new Employee
            {
                Name = employeeDTO.Name,
                Email = employeeDTO.Email,
                PhoneNumber = employeeDTO.PhoneNumber,
                Salary = employeeDTO.Salary,
                EmployeeDetails = new EmployeeDetails
                {
                    Address = employeeDTO.EmployeeDetails.Address,
                    FatherPhone = employeeDTO.EmployeeDetails.FatherPhone
                }
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Ok(new { Message = "Employee Created Successfully." });

        }
        [HttpGet]
        [Route("GetAnEmployeeById")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _context.Employees
                .Include(e => e.EmployeeDetails)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound(new { Message = "Employee not found." });

            var employeeDto = new EmployeeDTO
            {
                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                EmployeeDetails = new EmployeeDetailsDto
                {
                    Address = employee.EmployeeDetails.Address,
                    FatherPhone = employee.EmployeeDetails.FatherPhone
                }
            };

            return Ok(employeeDto);
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            var employee = _context.Employees
            .Include(e => e.EmployeeDetails)
            .ToList();

            var employeeDtos = employee.Select(e => new EmployeeDTO
            {
                Name = e.Name,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Salary = e.Salary,
                EmployeeDetails = new EmployeeDetailsDto
                {
                    Address = e.EmployeeDetails.Address,
                    FatherPhone = e.EmployeeDetails.FatherPhone
                }
            }).ToList();

            return Ok(employeeDtos);
        }
        [HttpPut]
        [Route("UpdateAnEmployeeById")]
        public IActionResult UpdateEmployeeById(int id, EmployeeDTO employeeDTO)
        {
            var employee = _context.Employees
                .Include(e => e.EmployeeDetails)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound(new { Message = "Employee not found." });

            employee.Name = employeeDTO.Name;
            employee.Email = employeeDTO.Email;
            employee.PhoneNumber = employeeDTO.PhoneNumber;
            employee.Salary = employeeDTO.Salary;

            if (employee.EmployeeDetails != null)
            {
                employee.EmployeeDetails.Address = employeeDTO.EmployeeDetails.Address;
                employee.EmployeeDetails.FatherPhone = employeeDTO.EmployeeDetails.FatherPhone;
            }

            _context.SaveChanges();
            return Ok(new { Message = "Employee Updated Successfully." });
        }

        [HttpDelete]
        [Route("DeleteAnEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees
                .Include(e => e.EmployeeDetails)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound(new { Message = "Employee not found." });

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Ok(new { Message = "Employee Deleted Successfully." });
        }
    }
}
