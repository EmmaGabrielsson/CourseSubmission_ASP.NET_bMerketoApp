namespace WebbApp.Models.Entities;

public class ShowcaseEntity
{
    public int Id { get; set; }
    public string? Ingress { get; set; }
    public string? Title { get; set; }
    public string? ImageUrl { get; set; }
    public string? LinkText { get; set; }
    public string? LinkUrl { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

}
