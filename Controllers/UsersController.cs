using DatabaseCommerce.Commands;
using DatabaseCommerce.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DatabaseCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginCommand command)
        {
            try
            {
                using var db = new ApplicationDbContext();

                var result = await db.Database
                    .SqlQuery<int>("EXECUTE sp_Login @p0, @p1", command.Login, command.Password)
                    .FirstOrDefaultAsync();

                return Ok(result);
            }
            catch(SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpAsync(SignUpCommand command)
        {
            try
            {
                using var db = new ApplicationDbContext();

                var result = await db.Database
                    .ExecuteSqlCommandAsync("EXECUTE sp_SignUp @p0, @p1, @p2, @p3, @p4",
                        command.FirstName, command.LastName, command.PhoneNumber, command.Email, command.Password);

                result += await db.Database
                    .ExecuteSqlCommandAsync("EXECUTE sp_AddAddress @p0, @p1, @p2, @p3, @p4, @p5, @p6",
                        command.Email, command.Town, command.Street, command.BuildingNumber, command.ApartmentNumber, command.ZipCode, command.Country);

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateAddressAsync(UpdateAddressCommand command)
        {
            try
            {
                using var db = new ApplicationDbContext();

                var result = await db.Database
                    .ExecuteSqlCommandAsync("EXECUTE sp_UpdateAddress @p0, @p1, @p2, @p3, @p4, @p5, @p6",
                        command.UserId, command.Town, command.Street, command.BuildingNumber, command.ApartmentNumber, command.ZipCode, command.Country);

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
