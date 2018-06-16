using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class OrderController : Controller
    {
        public ViewResult Checkout()
        {
            return View(new Order());
        }

        /*[HttpPost]
        public IActionResult Checkout(Order order)
        {

        }*/
    }
}