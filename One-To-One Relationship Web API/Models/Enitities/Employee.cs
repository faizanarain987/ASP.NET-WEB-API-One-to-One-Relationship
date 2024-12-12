namespace Test.Models.Enitities
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required decimal Salary { get; set; }

        // Navigation property for EmployeeDetails
        public required EmployeeDetails EmployeeDetails { get; set; }

    }
}
