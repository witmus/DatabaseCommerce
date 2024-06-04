using System.Data.Entity;
using System.Data.SqlClient;

namespace DatabaseCommerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() 
            : base("Server=localhost\\\\SQLEXPRESS,1433;Database=zbd_prod;User Id=zig;Password=ziguziguzig;TrustServerCertificate=true;")
        {

        }
    }
}
