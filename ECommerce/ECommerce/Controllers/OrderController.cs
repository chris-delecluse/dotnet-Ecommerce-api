using System;
using ECommerce.Dto;
using ECommerce.Exceptions;
using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;

    public OrderController(IOrderService orderService, IProductService productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ResQueryDto<List<OrderProduct>>>> Get()
    {
        IEnumerable<OrderProduct> orderProducts = await _orderService.GetAllOrderProduct();

        ResQueryDto<List<OrderProduct>> response = new(orderProducts.ToList(), orderProducts.Count());

        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ResQueryDto<List<OrderProduct>>>> GetOne(Guid id)
    {
        try
        {
            IQueryable<OrderProduct> orderProduct = _orderService.GetBySameId(id);

            ResQueryDto<List<OrderProduct>> response = new(
                await orderProduct.ToListAsync(),
                orderProduct.Count()
            );

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ResMutationDto<Order>>> Create(OrderDto dto)
    {
        try
        {
            IEnumerable<Product> products = await _productService.GetManyById(dto);

            Order order = await _orderService.InsertManyOrderProduct(products);

            ResMutationDto<Order> response = new("Resource added successfully", order);

            return Created("", response);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { ex.Message });
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