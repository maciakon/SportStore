using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductsRepository _repository;
        public NavigationMenuViewComponent(IProductsRepository repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke() 
        { 
            return View(_repository.Products.Select(x => x.Category).Distinct().OrderBy(x=>x));
        }
    }
}