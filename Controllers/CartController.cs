using DatabaseCommerce.Commands;
using DatabaseCommerce.Data;
using DatabaseCommerce.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DatabaseCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetUserCartAsync(int userId)
        {
            using var db = new ApplicationDbContext();

            try
            {
                var result = await db.Database
                    .SqlQuery<CartItemDto>(
                        "EXEC [dbo].[sp_GetCart] @p0", userId)
                    .ToListAsync();

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCartAsync(AddToCartCommand command)
        {
            using var db = new ApplicationDbContext();

            try
            {
                var result = await db.Database
                    .ExecuteSqlCommandAsync(
                        @"EXEC [dbo].[sp_AddToCart] @p0, @p1, @p2", command.UserId, command.ProductId, command.Amount);

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> ChangeCartItemAmountAsync(ChangeCartItemAmountCommand command)
        {
            using var db = new ApplicationDbContext();

            try
            {
                var result = await db.Database
                    .ExecuteSqlCommandAsync(
                        @"EXEC [dbo].[sp_ChangeCartItemAmount] @p0, @p1, @p2", command.UserId, command.ProductId, command.Amount);

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromCartAsync(int userId, int productId)
        {
            using var db = new ApplicationDbContext();

            try
            {
                var result = await db.Database
                    .ExecuteSqlCommandAsync(
                        @"EXEC [dbo].[sp_RemoveFromCart] @p0, @p1", userId, productId);

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
