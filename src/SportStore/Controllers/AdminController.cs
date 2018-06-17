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
    }
}