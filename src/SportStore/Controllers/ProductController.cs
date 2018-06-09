using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository _repository;

        public ProductController(IProductsRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List() => View(_repository.Products);
    }
}