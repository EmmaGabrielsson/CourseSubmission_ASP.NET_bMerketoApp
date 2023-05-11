namespace WebbApp.Models.Entities;

public class ProductReviewEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public string ArticleNumber { get; set;} = null!;
    public int Rating { get; set; } 
    public string? Comment { get; set; }
}
