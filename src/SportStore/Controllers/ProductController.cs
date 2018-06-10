using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize { get; set; } = 4;
        private readonly IProductsRepository _repository;

        public ProductController(IProductsRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List(int page = 1) => 
            View(
                new ProductListViewModel
                {
                    Products = _repository.Products
                            .OrderBy(product => product.ProductId)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),

                    PagingInfo = new PagingInfo {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = _repository.Products.Count()
                    }
                });
    }
}