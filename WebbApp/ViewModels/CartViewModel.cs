namespace WebbApp.ViewModels;

public class CartViewModel
{
    public ICollection<string>? CartList { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalPrice { get; set; }


}
