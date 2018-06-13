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

        public ViewResult List(string category, int page = 1) => 
            View(
                new ProductListViewModel
                {
                    Products = _repository.Products
                            .Where(product => category == null || product.Category == category)
                            .OrderBy(product => product.ProductId)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),

                    PagingInfo = new PagingInfo {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = category == null ?_repository.Products.Count() : 
                        _repository.Products.Where(product => product.Category == category).Count()
                    },

                    CurrentCategory = category
                });
    }
}