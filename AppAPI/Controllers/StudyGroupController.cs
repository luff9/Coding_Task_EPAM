using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    public class StudyGroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
