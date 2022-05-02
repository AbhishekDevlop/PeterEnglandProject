using Microsoft.AspNetCore.Mvc;

namespace PeterEngland2.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList() 
        {
            return View();
        }
    }
}
