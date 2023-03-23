using System;
using ECommerce.Dto;
using ECommerce.Models;

namespace ECommerce.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetOne(Guid id);
    Task<IEnumerable<Product>> GetManyById(OrderDto dto);
    Task<Product> InsertOne(ProductDto dto);
    Task<Product> UpdateOnePartially(Guid id, UpdateProductDto dto);
}