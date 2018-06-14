using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;

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
            var currentCategory = (string)RouteData?.Values["category"];
            var allCategories = _repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x);
            ViewBag.SelectedCategory = RouteData.Values["category"];
            return View(
                new NavigationMenuViewModel(allCategories, currentCategory));
        }
    }
}