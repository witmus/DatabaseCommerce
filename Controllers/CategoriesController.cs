using DatabaseCommerce.Commands;
using DatabaseCommerce.Data;
using DatabaseCommerce.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DatabaseCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            using var db = new ApplicationDbContext();

            try
            {
                var result = await db.Database
                    .SqlQuery<CategoryDto>(
                        "EXEC [dbo].[sp_GetCategories]")
                    .ToListAsync();

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {

            using var db = new ApplicationDbContext();

            try
            {
                var result = await db.Database
                    .ExecuteSqlCommandAsync(
                        @"EXEC [dbo].[sp_CreateCategory] @p0, @p1", command.Name, command.VatRate);

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
