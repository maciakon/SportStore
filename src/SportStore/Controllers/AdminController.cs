using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
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
    }
}