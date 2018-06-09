using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize { get; } = 4;
        private readonly IProductsRepository _repository;

        public ProductController(IProductsRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List(int page = 1) => 
            View(_repository.Products
                            .OrderBy(product => product.ProductId)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize)); 
    }
}