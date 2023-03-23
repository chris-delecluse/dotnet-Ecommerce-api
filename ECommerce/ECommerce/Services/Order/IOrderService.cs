using System;
using ECommerce.Models;

namespace ECommerce.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderProduct>> GetAllOrderProduct();
    IQueryable<OrderProduct> GetBySameId(Guid id);
    Task<Order> InsertManyOrderProduct(IEnumerable<Product> products);
}