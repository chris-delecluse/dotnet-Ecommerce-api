using System;
using ECommerce.Dto;
using ECommerce.Models;

namespace ECommerce.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetOne(Guid id);
    Task<IEnumerable<Product>> GetManyByIds(OrderDto dto);
    Task<Product> InsertOne(Product product);
    Task<Product> UpdateOnePartially(Product product);
}