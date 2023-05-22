using System.Diagnostics;
using System.Linq.Expressions;
using WebbApp.Migrations;
using WebbApp.Models.Dtos;
using WebbApp.Models.Entities;
using WebbApp.Repositories;

namespace WebbApp.Services;

public class OrderService
{
    #region Constructors & Private Fields 
    private readonly OrderRepo _orderRepo;
    private readonly OrderRowRepo _orderRowRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderService(OrderRepo orderRepo, OrderRowRepo orderRowRepo, IHttpContextAccessor httpContextAccessor)
    {
        _orderRepo = orderRepo;
        _orderRowRepo = orderRowRepo;
        _httpContextAccessor = httpContextAccessor;
    }
    #endregion

    public async Task<IEnumerable<OrderRowEntity>> GetOrderRowsAsync(Expression<Func<OrderRowEntity, bool>> expression)
    {
        try
        {
            var rows = await _orderRowRepo.GetAllDataAsync(expression);
            if (rows != null)
                return rows;

        }catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
    public async Task<OrderEntity> GetOrderAsync()
    {
        try
        {
            string orderId = _httpContextAccessor.HttpContext!.Session.GetString("OrderId")!;
            if (!string.IsNullOrEmpty(orderId))
            {
                var order = await _orderRepo.GetDataAsync(x => x.Id == Guid.Parse(orderId));
                if (order != null)
                    return order;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
    public async Task<OrderEntity> GetOrCreateOrderAndAddRowsAsync(Product addProduct)
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

            if (order != null)
            {
                order.TotalQuantity += addProduct.ProductQuantity;
                order.TotalPrice += (decimal)(addProduct.Price * addProduct.ProductQuantity)!;
                await _orderRepo.UpdateDataAsync(order);
            }
            else
            {
                order = new OrderEntity
                {
                    Id = Guid.Parse(orderId),
                    TotalQuantity = addProduct.ProductQuantity,
                    TotalPrice = (decimal)(addProduct.Price * addProduct.ProductQuantity)!
                };
                await _orderRepo.AddDataAsync(order);
            }
            if (await AddOrderRowAsync(orderId, addProduct))
                return order;

        } catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;

    }

    public async Task<bool> AddOrderRowAsync(string orderId, Product addProduct)
    {
        try
        {
            OrderRowEntity orderRow = await _orderRowRepo.GetDataAsync(x => x.OrderId == Guid.Parse(orderId) && x.ProductArticleNumber == addProduct.ArticleNumber);

            if (orderRow != null)
            {
                orderRow.Quantity = addProduct.ProductQuantity;
                await _orderRowRepo.UpdateDataAsync(orderRow);
            }
            else
            {
                orderRow = new OrderRowEntity
                {
                    OrderId = Guid.Parse(orderId),
                    ProductArticleNumber = addProduct.ArticleNumber!,
                    ProductPrice = (decimal)addProduct.Price!,
                    Quantity = addProduct.ProductQuantity
                };
                await _orderRowRepo.AddDataAsync(orderRow);
            }

            return true;
        } catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }


}
