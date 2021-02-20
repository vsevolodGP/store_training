using Microsoft.AspNetCore.Mvc;
using Store.Cashier.Areas.Cashier.Models;

namespace Store.Cashier.Areas.Cashier.Controllers
{
    [Area("Cashier")]
    public class HomeController : Controller
    {
        public IActionResult Index(int orderId, string returnUri)
        {
            var model = new ExampleModel
            {
                OrderId = orderId,
                ReturnUri = returnUri
            };

            return View(model);
        }

        // Cashier/Home/Callback
        public IActionResult CallBack(int orderId, string returnUri)
        {
            var model = new ExampleModel
            {
                OrderId = orderId,
                ReturnUri = returnUri
            };

            return View(model);
        }
    }
}
