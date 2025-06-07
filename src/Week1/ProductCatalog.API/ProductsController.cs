using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Products.Commands;
using ProductCatalog.Application.Products.Queries;

namespace ProductCatalog.API
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            _logger.LogInformation("Fetching all products.");
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductCommand command)
        {
            try
            {
                _logger.LogInformation("Creating product with SKU: {Sku}", command.Sku);
                var id = await _mediator.Send(command);

                return CreatedAtAction(
                    nameof(GetAll),
                    new { version = "1.0" },
                    new { id });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Invalid product data: {Message}", ex.Message);
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Product creation conflict: {Message}", ex.Message);
                return Conflict(new { error = ex.Message });
            }
        }
    }
}
