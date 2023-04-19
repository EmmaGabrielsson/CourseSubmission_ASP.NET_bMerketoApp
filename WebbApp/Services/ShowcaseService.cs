using WebbApp.Models;

namespace WebbApp.Services;

public class ShowcaseService
{
    private readonly List<ShowcaseModel> _showcases = new()
    {
        new ShowcaseModel()
        {
        Ingress = "WELCOME TO BMERKETO SHOP",
        Title = "Exclusive Chair gold Collection.",
        ImageUrl = "images/placeholders/625x647.svg",
        LinkText = "SHOP NOW",
        LinkUrl = "/products"
        }
    };

    public ShowcaseModel GetLatestShowcase()
    {
        return _showcases.LastOrDefault()!;
    }
}
