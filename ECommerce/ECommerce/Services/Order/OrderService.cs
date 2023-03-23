using System;
using ECommerce.Exceptions;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderProduct>> GetAllOrderProduct() =>
        await _orderRepository.GetAllOrderProduct();


    public IQueryable<OrderProduct> GetBySameId(Guid id)
    {
        IQueryable<OrderProduct> orderProducts = _orderRepository.GetOrderProductBySameOrderId(id);

        if (!orderProducts.Any())
            throw RequestProblemException.ForNotFound(id.ToString());

        return orderProducts;
    }

    public async Task<Order> InsertManyOrderProduct(IEnumerable<Product> products)
    {
        Order order = new();

        foreach (var product in products)
        {
            OrderProduct orderProduct = new OrderProduct
            {
                Order = order,
                Product = product
            };

            await _orderRepository.InsertOneOrderProduct(orderProduct);
        }

        await _orderRepository.InsertOneOrder(order);

        return order;
    }
}