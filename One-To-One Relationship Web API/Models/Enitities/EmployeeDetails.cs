namespace Test.Models.Enitities
{
    public class EmployeeDetails
    {
        public int Id { get; set; }
        public required string Address { get; set; }
        public required string FatherPhone { get; set; }


        // Foreign key for Employee
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
