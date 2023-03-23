using System;
using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ResQueryDto<IEnumerable<Product>>>> Get()
    {
        IEnumerable<Product> products = await _productService.GetAll();

        ResQueryDto<IEnumerable<Product>> response = new(products, products.Count());

        return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ResQueryDto<Product>>> GetOne(Guid id)
    {
        try
        {
            Product? product = await _productService.GetOne(id);

            ResQueryDto<Product> response = new(product!, 1);

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResMutationDto<Product>>> Create(ProductDto dto)
    {
        try
        {
            Product product = await _productService.InsertOne(dto);

            ResMutationDto<Product> response = new("Resource added successfully", product);

            return Created($"/api/product/{product.Id}", response);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { ex.Message });
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ResMutationDto<Product>>> UpdateOne(Guid id, UpdateProductDto dto)
    {
        try
        {
            Product product = await _productService.UpdateOnePartially(id, dto);

            ResMutationDto<Product> response = new("Resource updated succesffuly", product);

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { ex.Message });
        }
    }
}