using WebbApp.Models.Entities;

namespace WebbApp.Models.ViewModels;

public class ShowcaseViewModel
{
    public string? Ingress { get; set; }
    public string? Title { get; set; }
    public string? ImageUrl { get; set; }
    public string? LinkText { get; set; }
    public string? LinkUrl { get; set; }


    public static implicit operator ShowcaseEntity(ShowcaseViewModel model)
    {
        return new ShowcaseEntity
        {
            Ingress = model.Ingress,
            Title = model.Title,
            ImageUrl = model.ImageUrl,
            LinkText = model.LinkText,
            LinkUrl = model.LinkUrl
        };
    }

}
