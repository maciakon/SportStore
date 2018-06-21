using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductsRepository _repository;
        public AdminController(IProductsRepository repository)
        {
            _repository = repository;
        }
        public ViewResult Index() => View(_repository.Products);

        public ViewResult Edit(int productId)
        {
            var editedProduct = _repository.Products.FirstOrDefault(p => p.ProductId == productId);
            return View(editedProduct);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            // a simple client side validation is also implemented
            if(ModelState.IsValid)
            {
                _repository.Save(product);
                TempData["message"] = $"{product.Name} has been saved.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //something went wrong with saving a product
                return View(product);
            }
        }

        public IActionResult Delete(int productId)
        {
            if(ModelState.IsValid)
            {
                var removedProduct = _repository.Delete(productId);
                if(removedProduct != null)
                {
                    TempData["message"] = $"{removedProduct.Name} has been removed.";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public ViewResult Create() => View("Edit", new Product());
    }
}