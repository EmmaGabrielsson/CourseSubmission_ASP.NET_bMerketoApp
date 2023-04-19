using Microsoft.AspNetCore.Mvc;
using WebbApp.ViewModels;

namespace WebbApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Home";

        var _viewModel = new HomeIndexViewModel()
        {
            BestCollection = new GridCollectionViewModel 
            { 
                Title = "Best Collection",
                Categories = new List<string> {"All", "Bag", "Dress", "Decoration", "Essentials", "Interior", "Laptops", "Mobile", "Beauty"},
                GridItems = new List<GridCollectionItemViewModel>
                {
                    new GridCollectionItemViewModel { Id = "1", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "2", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "3", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "4", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "5", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "6", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "7", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "8", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 }
                }
            }
        };

        return View(_viewModel);
    }

}
