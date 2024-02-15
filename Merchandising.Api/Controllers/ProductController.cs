using Framework.Data.Pagination;
using MediatR;
using Merchandising.Application.Products.Commands.Create;
using Merchandising.Application.Products.Commands.Delete;
using Merchandising.Application.Products.Commands.Update;
using Merchandising.Application.Products.Commands.Update.Request;
using Merchandising.Application.Products.Dto;
using Merchandising.Application.Products.Queries.Get;
using Merchandising.Application.Products.Queries.GetById;
using Merchandising.Application.Products.Queries.GetLiveProducts;
using Microsoft.AspNetCore.Mvc;

namespace Merchandising.Api.Controllers;

[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Returns products
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<ProductDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Get([FromQuery] GetQuery request)
    {
        return Ok(await _mediator.Send(request));
    }
    
    
    /// <summary>
    /// Returns products
    /// </summary>
    [HttpGet("live")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<ProductDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetLives([FromQuery] GetLiveProductsQuery request)
    {
        return Ok(await _mediator.Send(request));
    }
    
    /// <summary>
    /// Returns product by id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdQuery(id)));
    }
    
    /// <summary>
    /// Create a new product
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<ProductDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> CreateProduct([FromBody] CreateCommand createCommand)
    {
        var product = await _mediator.Send(createCommand);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
    
    /// <summary>
    /// Update product by id
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<ProductDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateProductDto)
    {
        var request = new UpdateCommand(id, updateProductDto);
        
        var product = await _mediator.Send(request);

        return Ok(product);
    }
    
    
    /// <summary>
    /// Delete product by id
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<ProductDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var isDeleted = await _mediator.Send(new DeleteCommand(id));

        return Ok(isDeleted);
    }
}