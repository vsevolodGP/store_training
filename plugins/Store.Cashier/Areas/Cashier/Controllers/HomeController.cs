using Microsoft.AspNetCore.Mvc;

namespace Store.Cashier.Areas.Cashier.Controllers
{
    [Area("Cashier")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Cashier/Home/Callback
        public IActionResult CallBack()
        {
            return View();
        }
    }
}
