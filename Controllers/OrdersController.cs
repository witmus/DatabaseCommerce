using DatabaseCommerce.Commands;
using DatabaseCommerce.Data;
using DatabaseCommerce.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DatabaseCommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrdersByUserIdAsync(int userId)
        {
            using var db = new ApplicationDbContext();

            var result = await db.Database
                .SqlQuery<OrderHeaderDto>("EXEC [dbo].[sp_GetUserOrders] @p0", userId)
                .ToListAsync();

            return Ok(result);
        }

        [HttpPost("SubmitOrder")]
        public async Task<IActionResult> SubmitOrderAsync(SubmitOrderCommand command)
        {
            using var db = new ApplicationDbContext();

            try
            {
                var currentNumber = await db.Database
                    .SqlQuery<string>("EXEC [dbo].[sp_GetLastOrderNumber]")
                    .FirstOrDefaultAsync();

                var newNumber = $"F{int.Parse(currentNumber.Substring(2)) + 1}";

                var result = await db.Database
                    .ExecuteSqlCommandAsync(
                        "EXEC [dbo].[sp_SubmitOrder] @p0, @p1", newNumber, command.UserId);

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Invoice/{invoiceNumber}/{orderDate}")]
        public async Task<IActionResult> GetInvoiceAsync(string invoiceNumber, DateTime orderDate)
        {
            using var db = new ApplicationDbContext();

            var result = new InvoiceDataDto()
            {
                InvoiceNumber = invoiceNumber,
                OrderDate = orderDate
            };

            result.ReceiverWithAddress = await db.Database
                .SqlQuery<UserWithAddressDto>("EXEC [dbo].[sp_GetOrderReceiverWithAddress] @p0", invoiceNumber)
                .FirstOrDefaultAsync();

            result.OrderPositions = await db.Database
                .SqlQuery<OrderProductDto>("EXEC [dbo].[sp_GetOrderPositions] @p0", invoiceNumber)
                .ToListAsync();

            return Ok(result);
        }
    }
}
