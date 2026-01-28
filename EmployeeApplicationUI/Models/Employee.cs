namespace EmployeeApplicationUI.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal DailyRate { get; set; }
        public string WorkingDays { get; set; }
    }
}
