using System;
using ECommerce.DataAccess;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DatabaseContext _dbContext;

    public OrderRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<OrderProduct>> GetAllOrderProduct()
    {
        IQueryable<OrderProduct> orderproducts = _dbContext.OrderProducts
         .Include(o => o.Order)
         .Include(p => p.Product);

        return await orderproducts.ToListAsync();
    }

    public IQueryable<OrderProduct> GetOrderProductBySameOrderId(Guid id)
    {
        return _dbContext.OrderProducts
            .Where(op => op.Order!.Id == id)
            .Include(o => o.Order)
            .Include(p => p.Product);
    }

    public async Task<Order> InsertOneOrder(Order order)
    {
        await _dbContext.Order.AddAsync(order);

        return order;
    }

    public async Task<OrderProduct> InsertOneOrderProduct(OrderProduct orderProduct)
    {
        await _dbContext.OrderProducts.AddAsync(orderProduct);
        await _dbContext.SaveChangesAsync();

        return orderProduct;
    }
}