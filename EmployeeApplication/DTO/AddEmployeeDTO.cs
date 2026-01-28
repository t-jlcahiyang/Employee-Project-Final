using System.ComponentModel.DataAnnotations;

namespace EmployeeApplication.DTO
{
    public class AddEmployeeDTO
    {
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public decimal DailyRate { get; set; }
        public string WorkingDays { get; set;  }
    }
}
