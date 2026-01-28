using Microsoft.AspNetCore.Mvc;

namespace EmployeeApplicationUI.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
