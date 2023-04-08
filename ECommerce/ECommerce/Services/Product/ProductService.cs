using System;
using ECommerce.DataAccess;
using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Models;
using ECommerce.Repositories;

namespace ECommerce.Services;

public class ProductService : IProductService
{
    private readonly DatabaseContext _dbContext;
    private readonly IProductRepository _productRepository;

    public ProductService(DatabaseContext dbContext, IProductRepository productRepository)
    {
        _dbContext = dbContext;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _productRepository.GetAll();
    }

    public async Task<IEnumerable<Product>> GetManyById(OrderDto dto)
    {
        if (!dto.ProductIds.Any())
            throw RequestProblemException.ForMissingField("productIds");

        IEnumerable<Product> products = await _productRepository.GetManyByIds(dto);

        if (dto.ProductIds.Count() != products.Count())
            throw RequestProblemException.ForNotFound(nameof(Product));

        return products;
    }

    public async Task<Product?> GetOne(Guid id)
    {
        Product? product = await _productRepository.GetOne(id);

        if (product is null)
            throw RequestProblemException.ForNotFound(id.ToString());

        return product;
    }

    public async Task<Product> InsertOne(ProductDto dto)
    {
        ProductQuantities quantities = new()
        {
            Quantities = dto.Quantities
        };

        Product product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Quantities = quantities
        };

        return await _productRepository.InsertOne(product);
    }

    public async Task<Product> UpdateOnePartially(Guid id, UpdateProductDto dto)
    {
        Product? product = await _productRepository.GetOne(id);

        if (product is null)
            throw RequestProblemException.ForNotFound(id.ToString());

        product.Name = dto.Name ?? product.Name;
        product.Description = dto.Description ?? product.Description;
        product.Price = dto.Price ?? product.Price;

        return await _productRepository.UpdateOnePartially(product);
    }
}