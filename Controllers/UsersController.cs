using DatabaseCommerce.Commands;
using DatabaseCommerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginCommand command)
        {
            using var db = new ApplicationDbContext();

            var result = await db.Database
                .SqlQuery<string>("EXECUTE sp_SignIn @p0, @p1", command.Login, command.Password)
                .FirstOrDefaultAsync();

            return Ok(result);
        }
    }
}
