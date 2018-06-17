using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly Cart _cart;

        public OrderController(IOrdersRepository ordersRepository, Cart cart)
        {
            _ordersRepository = ordersRepository;
            _cart = cart;
        }

        public ViewResult List() => View(_ordersRepository.Orders.Where(o => !o.Shipped));

        public ViewResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult MarkShipped(int orderId)
        {
            var order = _ordersRepository.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if(order != null)
            {
                order.Shipped = true;
                _ordersRepository.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if(_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
                
            }
            if(ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                 _ordersRepository.SaveOrder(order);
                 return RedirectToAction(nameof(Completed));
            }
            return View(order);
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}