using System;
using ECommerce.Models;

namespace ECommerce.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<OrderProduct>> GetAllOrderProduct();
    IQueryable<OrderProduct> GetOrderProductBySameOrderId(Guid id);
    Task<Order> InsertOneOrder(Order order);
    Task<OrderProduct> InsertOneOrderProduct(OrderProduct orderProduct);
}