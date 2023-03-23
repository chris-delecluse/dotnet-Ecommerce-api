using System;
using ECommerce.DataAccess;
using ECommerce.Dto;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DatabaseContext _dbContext;

    public ProductRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _dbContext.Product
            .Include(p => p.Quantities)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetManyByIds(OrderDto dto)
    {
        return await _dbContext.Product
            .Where(p => dto.productIds.Contains(p.Id))
            .ToListAsync();
    }

    public async Task<Product?> GetOne(Guid id)
    {
        return await _dbContext.Product
            .Include(p => p.Quantities)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> InsertOne(Product product)
    {
        await _dbContext.Product.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateOnePartially(Product product)
    {
        _dbContext.Product.Update(product);
        await _dbContext.SaveChangesAsync();

        return product;
    }
}