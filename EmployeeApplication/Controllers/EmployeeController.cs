using EmployeeApplication.DTO;
using EmployeeApplication.Models;
using EmployeeApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        //[HttpGet("test")]
        //public IActionResult Get()
        //{
        //    return Ok("Sample test working");
        //}

        private readonly IEmployeeRepository _repo;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository repo, ILogger<EmployeeController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var list = _repo.GetEmployeeList();
            return Ok(list);

            //var formatList = list.Select(e => new
            //{
            //    e.ID,
            //    e.EmployeeNumber,
            //    e.FirstName,
            //    e.MiddleName,
            //    e.LastName,
            //    BirthDate = e.BirthDate.ToString("MMMM dd, yyyy"),
            //    e.DailyRate,
            //    e.WorkingDays
            //}).ToList();

            //return Ok(formatList);
        }

        [HttpPost]
        public IActionResult OnPostAddEmployee(AddEmployeeDTO employeedto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = new Employee
            {
                FirstName = employeedto.FirstName,
                MiddleName = employeedto.MiddleName,
                LastName = employeedto.LastName,
                BirthDate = employeedto.BirthDay,
                DailyRate = employeedto.DailyRate,
                WorkingDays = employeedto.WorkingDays
            };

            var result = _repo.InsertEmployee(employee);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, "Cant insert");
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult OnPutEditEmployee(int id, EditEmployeeDTO updated)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = new Employee
            {
                ID = updated.Id,
                FirstName = updated.FirstName,
                MiddleName = updated.MiddleName,
                LastName = updated.LastName,
                BirthDate = updated.BirthDay,
                DailyRate = updated.DailyRate,
                WorkingDays = updated.WorkingDays
            };

            var result = _repo.UpdateEmployee(employee);
            if (result)
            {
                return Ok(result);
            }
            else
            {

                return StatusCode(500, "Cant update");
            }
        }


        [HttpDelete("{id:int}")]
        public IActionResult OnDeleteEmployee(int id)
        {
            var result = _repo.DeleteEmployee(id);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, "failed to delete");
            }
        }

        [HttpPost("TakeHomePay")]
        public IActionResult PostTakeHomePay(TakeHomePayDTO THP)
        {
            try
            {
                var takeHomePay = _repo.ComputeTakeHomePay(THP.StartDate, THP.EndDate, THP.DailyRate);
                return Ok(new { TakeHomePay = takeHomePay });
            }
            catch(Exception ex)
            {
                Console.WriteLine("error", ex);
               return StatusCode(500, "failed to show data");
            }
        }

    }

}
