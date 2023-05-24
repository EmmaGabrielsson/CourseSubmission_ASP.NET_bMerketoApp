using System.Diagnostics;
using WebbApp.Models.Dtos;
using WebbApp.Models.Entities;
using WebbApp.Repositories;

namespace WebbApp.Services;

public class OrderService
{
    #region Constructors & Private Fields 
    private readonly OrderRepo _orderRepo;
    private readonly OrderRowRepo _orderRowRepo;
    private readonly ProductService _productService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly StockRepo _stockRepo;

    public OrderService(OrderRepo orderRepo, OrderRowRepo orderRowRepo, IHttpContextAccessor httpContextAccessor, ProductService productService, StockRepo stockRepo)
    {
        _orderRepo = orderRepo;
        _orderRowRepo = orderRowRepo;
        _httpContextAccessor = httpContextAccessor;
        _productService = productService;
        _stockRepo = stockRepo;
    }
    #endregion

    public async Task<IEnumerable<OrderRow>> GetOrderRowsAsync(string orderId)
    {
        try
        {
            var rows = await _orderRowRepo.GetAllDataAsync(x => x.OrderId == Guid.Parse(orderId));
            if (rows != null)
            {
                var rowList = new List<OrderRow>();
                foreach (var row in rows)
                {
                    var newRow = new OrderRow
                    {
                        OrderId = row.OrderId,
                        ProductArticleNumber = row.ProductArticleNumber,
                        ProductName = (await _productService.GetAsync(x => x.ArticleNumber == row.ProductArticleNumber)).ProductName,
                        Quantity = row.Quantity,
                        ProductPrice = row.ProductPrice,
                        StockQuantity = (await _stockRepo.GetDataAsync(x => x.ProductArticleNumber == row.ProductArticleNumber)).Quantity,
                        ImageUrl = (await _productService.GetAsync(x => x.ArticleNumber == row.ProductArticleNumber)).ImageUrl,
                        Discount = row.Discount,
                    };
                    if(row != null)
                        rowList.Add(newRow);
                }

            return rowList;
            }

        }catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
    public async Task<Order> GetOrCreateOrderAsync()
    {
        try
        {
            // Check if the order ID exists in the session, if not create orderId
            string orderId = _httpContextAccessor.HttpContext!.Session.GetString("OrderId")!;
            if (orderId == null)
            {
                orderId = Guid.NewGuid().ToString();
                _httpContextAccessor.HttpContext.Session.SetString("OrderId", orderId);
            }

            OrderEntity order = await _orderRepo.GetDataAsync(x => x.Id == Guid.Parse(orderId));

            int totalQty = 0;
            decimal totalPrice = 0;

            if (order == null) 
            { 
                order = new OrderEntity
                {
                    Id = Guid.Parse(orderId),
                    TotalQuantity = totalQty,
                    TotalPrice = totalPrice,
                };
                await _orderRepo.AddDataAsync(order);
            }
            else
            {
                if((await _orderRowRepo.GetAllDataAsync(x => x.OrderId == Guid.Parse(orderId))) != null)
                {
                    foreach (var item in (await _orderRowRepo.GetAllDataAsync(x => x.OrderId == Guid.Parse(orderId))))
                    {
                        if(item != null)
                        {
                            totalQty += item.Quantity;
                            totalPrice += item.ProductPrice * item.Quantity;
                        }
                    }

                }
                order.TotalPrice = totalPrice;
                order.TotalQuantity = totalQty;
                await _orderRepo.UpdateDataAsync(order);
            }
            return order;

        } catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;

    }
    public async Task<bool> AddOrderRowAsync(Guid orderId, Product addProduct)
    {
        try
        {
            OrderRowEntity orderRow = await _orderRowRepo.GetDataAsync(x => x.OrderId == orderId && x.ProductArticleNumber == addProduct.ArticleNumber);

            if (orderRow != null)
            {
                orderRow.Quantity = addProduct.ProductQuantity;
                await _orderRowRepo.UpdateDataAsync(orderRow);
            }
            else
            {
                orderRow = new OrderRowEntity
                {
                    OrderId = orderId,
                    ProductArticleNumber = addProduct.ArticleNumber!,
                    ProductPrice = (decimal)addProduct.Price!,
                    Quantity = addProduct.ProductQuantity,
                    Discount = (decimal)addProduct.Discount!
                };
                await _orderRowRepo.AddDataAsync(orderRow);
            }

            return true;
        } catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }
    public async Task<bool> AddOrderRowAsync(Guid orderId, string articleNumber)
    {
        try
        {
            OrderRowEntity orderRow = await _orderRowRepo.GetDataAsync(x => x.OrderId == orderId && x.ProductArticleNumber == articleNumber);

            if (orderRow != null)
            {
                await _orderRowRepo.UpdateDataAsync(orderRow);
            }
            else
            {
                var findStock = await _stockRepo.GetDataAsync(x => x.ProductArticleNumber == articleNumber);
                orderRow = new OrderRowEntity
                {
                    OrderId = orderId,
                    ProductArticleNumber = articleNumber!,
                    ProductPrice = findStock.Price,
                    Quantity = 1
                };
                await _orderRowRepo.AddDataAsync(orderRow);
            }

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

}
