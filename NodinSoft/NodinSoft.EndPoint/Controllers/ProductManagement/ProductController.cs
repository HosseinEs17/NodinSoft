using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NodinSoft.Application.ProductManagement.Commands;
using NodinSoft.Application.ProductManagement.Queries;
using NodinSoft.Common.Enum;
using NodinSoft.Common.Model;
using NodinSoft.Entities.ProductManagement;
using System.Security.Claims;

namespace NodinSoft.EndPoint.Controllers.ProductManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            try
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string? email = User.FindFirstValue(ClaimTypes.Email);
                string? phoneNumber = User.FindFirstValue("phone_number");

                command.UserId = Convert.ToInt32(userId);
                command.ManufactureEmail = email!;
                command.ManufacturePhone = phoneNumber!;

                int productId = await _mediator.Send(command);

                return Ok(new { ProductId = productId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                GetProductsQuery query = new GetProductsQuery();
                return Ok(await _mediator.Send(query));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetFilteredProducts(int userId)
        {
            try
            {
                GetFilteredProductsByUserIdQuery query = new GetFilteredProductsByUserIdQuery() { UserId = userId };
                IEnumerable<Product> products = await _mediator.Send(query);
                if (products == null || products.Count() is 0)
                    return NotFound();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<MethodExecutionInfo>> DeleteProduct(int id)
        {
            MethodExecutionInfo result = new MethodExecutionInfo();
            try
            {
                DeleteProductCommand command = new DeleteProductCommand();
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                command.Id = id;
                command.UserId = Convert.ToInt32(userId);
                result = await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Result = null;
                result.Status = ApiStatus.InternalServerError;
            }
            return CreateActionResult(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<MethodExecutionInfo>> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
        {
            MethodExecutionInfo result = new MethodExecutionInfo();
            try
            {
                if (id != command.Id)
                {
                    result.Success = false;
                    result.Message = "Product ID mismatch.";
                    result.Result = null;
                    result.Status = ApiStatus.BadRequest;
                    return CreateActionResult(result);
                }
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                command.UserId = Convert.ToInt32(userId);
                result = await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Result = null;
                result.Status = ApiStatus.InternalServerError;
            }
            return CreateActionResult(result);
        }

        private ActionResult<MethodExecutionInfo> CreateActionResult(MethodExecutionInfo result)
        {
            switch (result.Status)
            {
                case ApiStatus.OK:
                    return Ok(result);

                case ApiStatus.Created:
                    return Created();

                case ApiStatus.Accepted:
                    return Accepted(result);

                case ApiStatus.NoContent:
                    return NoContent();

                case ApiStatus.BadRequest:
                    return BadRequest(result);

                case ApiStatus.Unauthorized:
                    return Unauthorized(result);

                case ApiStatus.Forbidden:
                    return Forbid();

                case ApiStatus.NotFound:
                    return NotFound(result);

                case ApiStatus.MethodNotAllowed:
                    return StatusCode((int)ApiStatus.MethodNotAllowed);

                case ApiStatus.Conflict:
                    return StatusCode((int)ApiStatus.Conflict);

                case ApiStatus.InternalServerError:
                    return StatusCode((int)ApiStatus.InternalServerError);

                default:
                    return StatusCode((int)result.Status);
            }
        }
    }
}
