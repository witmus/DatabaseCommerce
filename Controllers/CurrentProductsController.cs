using DatabaseCommerce.Commands;
using DatabaseCommerce.Data;
using DatabaseCommerce.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DatabaseCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrentProductsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(CreateProductCommand command)
        {
            using var db = new ApplicationDbContext();

            try
            {
                var result = await db.Database
                    .ExecuteSqlCommandAsync(
                        @"EXEC [dbo].[sp_InsertCurrentProduct]
                        @p0, @p1, @p2, @p3",
                        command.Name, command.NetPrice, command.IsDiscount, command.CategoryName);

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            using var db = new ApplicationDbContext();

            var result = await db.Database
                .SqlQuery<CurrentProductDto>("EXEC [dbo].[sp_GetCurrentProducts]")
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("{categoryName}")]
        public async Task<IActionResult> GetProductsByCategoryName(string categoryName)
        {
            using var db = new ApplicationDbContext();

            var result = await db.Database
                .SqlQuery<CurrentProductDto>("EXEC [dbo].[sp_GetCurrentProductsByCategoryName] @p0", categoryName)
                .ToListAsync();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync(UpdateProductCommand command)
        {
            using var db = new ApplicationDbContext();

            try
            {
                var result = await db.Database
                    .ExecuteSqlCommandAsync(
                        @"EXEC [dbo].[sp_UpdateCurrentProduct]
                        @p0, @p1, @p2, @p3",
                        command.Id, command.Name, command.NetPrice, command.IsDiscount);

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            using var db = new ApplicationDbContext();

            try
            {
                var result = await db.Database
                    .ExecuteSqlCommandAsync("EXEC [dbo].[sp_DeleteCurrentProduct] @p0", id);

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
