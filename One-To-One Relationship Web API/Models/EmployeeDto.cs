using Test.Models.Enitities;

namespace Test.Models
{
    public class EmployeeDTO
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required decimal Salary { get; set; }
        public required EmployeeDetailsDto EmployeeDetails { get; set; }
    }
}
