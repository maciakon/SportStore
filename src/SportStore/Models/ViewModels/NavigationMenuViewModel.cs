using System.Collections.Generic;

namespace SportStore.Models.ViewModels
{
    public class NavigationMenuViewModel
    {
        public IEnumerable<string> Categories { get; set; }
        public string CurrentCategory { get; set; }
        public NavigationMenuViewModel(IEnumerable<string> categories, string currentCategory)
        {
            CurrentCategory = currentCategory;
            Categories = categories;

        }
    }
}